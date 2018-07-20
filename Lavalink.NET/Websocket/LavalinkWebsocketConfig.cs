using System;
using System.Collections.Generic;
using System.Text;
using Lavalink.NET.Node;

namespace Lavalink.NET.Websocket
{
	internal class LavalinkWebsocketConfig
	{
		internal LavalinkNodeInfo NodeInfo { get; set; }
		internal string UserID { get; set; }
		internal string ShardCount { get; set; }
	}
}
