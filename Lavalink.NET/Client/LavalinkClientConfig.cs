using System;
using System.Collections.Generic;
using System.Text;
using Lavalink.NET.Logging;
using Lavalink.NET.Node;

namespace Lavalink.NET.Client
{
    public class LavalinkClientConfig
    {
		/// <summary>
		/// The Lavalink Nodes this LavalinkClient should use.
		/// </summary>
		public LavalinkNodeInfo[] Nodes { get; set; }

		/// <summary>
		/// The UserID of your Bot.
		/// </summary>
		public ulong UserID { get; set; }

		/// <summary>
		/// The ShardCount of your Bot.
		/// </summary>
		public int ShardCount { get; set; } = 1;

		/// <summary>
		/// determine if the build in Logging module should be used.
		/// </summary>
		public bool UseLogging { get; set; } = false;

		/// <summary>
		/// The LogLevel of this Client.
		/// </summary>
		public LogLevel LogLevel { get; set; } = LogLevel.Info;
	}
}
