using System;
using System.Threading;
using System.Threading.Tasks;
using Lavalink.NET.Client;
using Lavalink.NET.Websocket;

namespace Lavalink.NET.Node
{
    internal class LavalinkNode
    {
		public bool Connected
			=> _ws.Connected;

		public Region Region
			=> Info.Region;

		public LavalinkNodeInfo Info { get; }

		private LavalinkWebsocket _ws;
		private Thread _wsThread;
		private readonly LavalinkClient _client;

		internal LavalinkNode(LavalinkClient client, LavalinkNodeInfo info)
		{
			_client = client;
			Info = info;
			_ws = new LavalinkWebsocket(new LavalinkWebsocketConfig
			{
				NodeInfo = Info,
				UserID = _client.Config.UserID.ToString(),
				ShardCount = _client.Config.ShardCount.ToString()
			});
			_wsThread = new Thread(new ThreadStart(_ws.Connect));
		}

		internal Task<bool> ConnectAsync()
		{
			var tcs = new TaskCompletionSource<bool>();

			_ws.Ready += () => tcs.TrySetResult(true);
			_ws.ConnectionFailed += (Exception e) => tcs.TrySetException(e);

			_wsThread.Start();

			return tcs.Task;
		}

		internal Task DisconnectAsync()
			=> _ws.DisconnectAsync();
    }
}
