using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lavalink.NET.Websocket
{
	internal class LavalinkWebsocket : IDisposable
	{
		internal event Action<WebSocketCloseStatus, string> Disconnect;
		internal event Action<Exception> ConnectionFailed;
		internal event Action<string> LogMessage;
		internal event Action<string> Message;
		internal event Action Ready;

		internal bool Connected = false;

		private const int ReceiveChunkSize = 1024;
		private const int SendChunkSize = 1024;

		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly CancellationToken _cancellationToken;
		private readonly ClientWebSocket _ws;
		private readonly Uri _uri;

		internal LavalinkWebsocket(LavalinkWebsocketConfig options)
		{
			_ws = new ClientWebSocket();
			_ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);
			_ws.Options.SetRequestHeader("Authorization", options.NodeInfo.Authorization);
			_ws.Options.SetRequestHeader("Num-Shards", options.ShardCount);
			_ws.Options.SetRequestHeader("User-Id", options.UserID);
			_uri = new Uri(options.NodeInfo.WebsocketHost);
			_cancellationToken = _cancellationTokenSource.Token;
		}

		public void Dispose()
		{
			((IDisposable) _ws).Dispose();
			_cancellationTokenSource.Dispose();
		}

		internal async void Connect()
		{
			try
			{
				await _ws.ConnectAsync(_uri, _cancellationToken);
			}
			catch (Exception e)
			{
				ConnectionFailed?.Invoke(e);
				return;
			}
			Debug?.Invoke("Websocket Connection succesfully established");
			Ready?.Invoke();
			Connected = true;
			StartListen();
		}

		internal async Task DisconnectAsync()
		{
			if (_ws.State == WebSocketState.Open)
			{
				await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, _cancellationToken);
				Connected = false;
				Dispose();
			}
		}

		internal async Task SendMessageAsync(string message)
		{
			if (_ws.State != WebSocketState.Open)
			{
				throw new Exception("Connection is not open.");
			}

			byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
			int messagesCount = (int) Math.Ceiling((double) messageBuffer.Length / SendChunkSize);

			for (var i = 0; i < messagesCount; i++)
			{
				int offset = (SendChunkSize * i);
				int count = SendChunkSize;
				bool lastMessage = ((i + 1) == messagesCount);

				if ((count * (i + 1)) > messageBuffer.Length)
				{
					count = messageBuffer.Length - offset;
				}

				await _ws.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, _cancellationToken);
			}
		}

		private async void StartListen()
		{
			byte[] buffer = new byte[ReceiveChunkSize];

			try
			{
				while (_ws.State == WebSocketState.Open)
				{
					StringBuilder stringResult = new StringBuilder();

					WebSocketReceiveResult result;
					do
					{
						result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationToken);

						if (result.MessageType == WebSocketMessageType.Close)
						{
							await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _cancellationToken);
							Disconnect?.Invoke((WebSocketCloseStatus) _ws.CloseStatus, _ws.CloseStatusDescription);
						}
						else
						{
							string str = Encoding.UTF8.GetString(buffer, 0, result.Count);
							stringResult.Append(str);
						}

					} while (!result.EndOfMessage);

					var resultAsString = stringResult.ToString();

					if (resultAsString.Length > 0) Message?.Invoke(resultAsString);
				}
			}
			catch (Exception)
			{
				await DisconnectAsync();
				Disconnect?.Invoke((WebSocketCloseStatus) _ws.CloseStatus, _ws.CloseStatusDescription);
			}
			finally
			{
				_ws.Dispose();
			}
		}
	}
}