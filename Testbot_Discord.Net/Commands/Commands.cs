﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Lavalink.NET.Types;

namespace Testbot_Discord.Net.Commands
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
		[Command("hi")]
		public async Task Hi()
		{
			await Context.Channel.SendMessageAsync($"👋 Hi, {Context.User.Mention}!");
		}

		[Command("join")]
		public async Task Join()
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			await player.JoinAsync(Context.Guild.GetUser(Context.User.Id).VoiceChannel.Id.ToString());
		}

		[Command("leave")]
		public async Task Leave()
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			await player.LeaveAsync();
		}

		[Command("play")]
		public async Task Play(string query)
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			List<Track> tracks = await Client._lavalinkClient.LoadTracksAsync(query);
			await player.PlayAsync(tracks[0]);
		}

		[Command("pause")]
		public async Task Pause()
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			await player.PauseAsync();
		}

		[Command("resume")]
		public async Task Resume()
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			await player.PauseAsync(false);
		}

		[Command("joinchannel")]
		public async Task JoinSpecificChannel(SocketChannel channel)
		{
			Player player = Client._lavalinkClient.Players.GetPlayer(Context.Guild.Id.ToString());
			await player.JoinAsync(channel.Id.ToString());
		}
	}
}