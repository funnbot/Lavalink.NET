using System;
using System.Collections.Generic;
using Discord.WebSocket;
using Lavalink.NET.Client;

namespace Lavalink.NET.Node
{
    internal class LavalinkNodeManager
    {
		private readonly List<LavalinkNode> _nodes = new List<LavalinkNode>();
		private readonly LavalinkClient _client;
		private readonly Dictionary<string, Region> _regions = new Dictionary<string, Region>();
		private readonly Random _random = new Random();

		internal LavalinkNodeManager(LavalinkClient client)
		{
			_client = client;
			_regions.Add("sydney", Region.ASIA);
			_regions.Add("singapore", Region.ASIA);
			_regions.Add("japan", Region.ASIA);
			_regions.Add("hongkong", Region.ASIA);
			_regions.Add("london", Region.EUROPE);
			_regions.Add("frankfurt", Region.EUROPE);
			_regions.Add("amsterdam", Region.EUROPE);
			_regions.Add("russia", Region.EUROPE);
			_regions.Add("eu-central", Region.EUROPE);
			_regions.Add("eu-west", Region.EUROPE);
			_regions.Add("us-central", Region.AMERICA);
			_regions.Add("us-west", Region.AMERICA);
			_regions.Add("us-east", Region.AMERICA);
			_regions.Add("us-south", Region.AMERICA);
			_regions.Add("brazil", Region.AMERICA);
		}

		internal void AddNode(LavalinkNodeInfo info)
		{
			var lavalinknode = new LavalinkNode(_client, info);
			if (!_nodes.Contains(lavalinknode)) _nodes.Add(lavalinknode);
		}

		internal void RemoveNode(LavalinkNodeInfo info)
		{
			var node = _nodes.Find((LavalinkNode testNode) => testNode.Info == info);
			if (_nodes.Contains(node)) _nodes.Remove(node);
		}

		internal LavalinkNode[] GetNodes
			=> _nodes.ToArray();
		
		internal LavalinkNode GetBestNode(ulong guildID)
		{
			SocketGuild guild = _client.DiscordClient.GetGuild(guildID);

			var guildRegion = guild.VoiceRegionId;

			var guildRegionFormatted = guildRegion.Replace("vip-", "");

			var knownRegion = _regions.TryGetValue(guildRegionFormatted, out Region region);

			if (!knownRegion) return _nodes[0];

			var bestNodes = _nodes.FindAll((LavalinkNode node) => node.Region == region);

			return bestNodes.Count == 0 ? _nodes[0] : bestNodes[(int) Math.Round(_random.NextDouble() * bestNodes.Count)];
		}
	}
}
