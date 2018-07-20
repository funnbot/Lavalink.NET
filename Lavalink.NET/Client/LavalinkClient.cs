using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.WebSocket;
using Lavalink.NET.Node;
using Lavalink.NET.Player;
using Lavalink.NET.Types;
using Newtonsoft.Json;
using Serilog;

namespace Lavalink.NET.Client
{
	public class LavalinkClient
	{
		internal readonly BaseSocketClient DiscordClient;
		internal readonly LavalinkClientConfig Config;

		internal ILogger Logger { get; }

		private readonly HttpClient _httpClient = new HttpClient();
		private readonly Dictionary<ulong, LavalinkPlayer> _players = new Dictionary<ulong, LavalinkPlayer>();
		private LavalinkNodeManager _manager;


		public LavalinkClient(LavalinkClientConfig config, DiscordSocketClient client)
		{
			Config = config;
			DiscordClient = client;

			Configure();
		}

		public LavalinkClient(LavalinkClientConfig config, DiscordShardedClient client)
		{
			Config = config;
			DiscordClient = client;

			Configure();
		}

		public async Task<List<Track>> GetTracksAsync(string identifier)
		{
			var json = await _httpClient.GetStringAsync($"{Config.Nodes[0].RestHost}/loadtracks?identifier={identifier}" );
			return JsonConvert.DeserializeObject<List<Track>>(json);
		}

		public async Task<List<Track>> SearchYoutubeAsync(string query)
		{
			var json = await _httpClient.GetStringAsync($"{Config.Nodes[0].RestHost}/loadtracks?identifier=ytsearch: {query}");
			return JsonConvert.DeserializeObject<List<Track>>(json);
		}

		public void AddNode(LavalinkNodeInfo info)
			=> _manager.AddNode(info);

		public void RemoveNode(LavalinkNodeInfo info)
			=> _manager.RemoveNode(info);

		public Task StartAsync()
		{
			
		}

		public Task DestroyAsync()
		{

		}

		private void Configure()
		{
			_httpClient.DefaultRequestHeaders.Add("Authorization", Config.Nodes[0].Authorization);
			_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			_manager = new LavalinkNodeManager(this);

			if (Config.UseLogging) new LoggerConfiguration().MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch((Serilog.Events.LogEventLevel) Config.LogLevel)).WriteTo.Console().CreateLogger();

			foreach (var NodeInfo in Config.Nodes)
			{
				_manager.AddNode(NodeInfo);
			}
		}


	}
}
