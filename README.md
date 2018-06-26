Lavalink.NET

A Library to interact with Lavalink, working with Any Discord APi library for C#.

How to use the Library 

Extend the Client and implement your own SendAsync() method to either forward data to the Discord Websocket or handle the externally connection to a VoiceChannel

Example implementation with Discord.Net
```CSharp
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Lavalink.NET;
using Lavalink.NET.Types;

namespace Bot.Music
{
	class LavalinkClient : Lavalink.NET.Client
	{
		private DiscordSocketClient _client;

		public LavalinkClient(ClientOptions options, DiscordSocketClient client) 
			: base(options)
		{
			_client = client;
		}

		public override async Task SendAsync(DiscordOP4Packet packet)
		{
			if (_client.GetGuild(packet.DiscordVoicePacket.GuildID) != null) {
				if (packet.DiscordVoicePacket.ChannelID != null) {
					SocketChannel channel = _client.GetChannel(packet.DiscordVoicePacket.ChannelID ?? default(ulong));

					if (!(channel is IAudioChannel voicechannel)) throw new Exception("Wrong channel type.");

					await voicechannel.ConnectAsync(false, false, true);
				} else
				{
					SocketChannel channel = _client.GetChannel(packet.DiscordVoicePacket.ChannelID ?? default(ulong));

					if (!(channel is IAudioChannel voicechannel)) throw new Exception("Wrong channel type.");

					await voicechannel.DisconnectAsync();
				}
			}
		}
	}
}
```

Example implementation with DSharpPlus (notice that you need to have a Singleton for Websockets and Serialize the DiscordOP4Packet because there is no way to connect externally to VoiceChannels with it)

```CSharp
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Net.WebSocket;
using Lavalink.NET;
using Lavalink.NET.Types;
using Newtonsoft.Json;

namespace Bot.Music
{
	public class LavalinkClient : Lavalink.NET.Client
	{
		private DiscordClient _client;

		public LavalinkClient(ClientOptions options, DiscordClient client)
			: base(options)
		{
			_client = client;
		}

		public override Task SendAsync(DiscordOP4Packet packet)
		{
			if (_client.Guilds.ContainsKey(packet.DiscordVoicePacket.GuildID))
			{
				WebsocketStorage.storage.TryGetValue(1, out BaseWebSocketClient ws);
				ws.SendMessage(JsonConvert.SerializeObject(packet));
			}
			return Task.CompletedTask;
		}
	}
}
```

For more examples look in TestBot folders depending on your library.