
namespace Lavalink.NET.Node
{
    public class LavalinkNodeInfo
    {
		/// <summary>
		/// The Host of this node (without protocol and Port).
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// The Formatted Websocket Host with Protocol, Host and Port included
		/// </summary>
		public string WebsocketHost
			=> $"ws://{Host}:{WebsocketPort}";

		/// <summary>
		/// The Formatted Rest Host with Protocol, Host and Port included
		/// </summary>
		public string RestHost
			=> $"http://{Host}:{RestPort}";

		/// <summary>
		/// The Authorization Password for this Node.
		/// </summary>
		public string Authorization { get; set; }

		/// <summary>
		/// The Port for the Websocket Server of this Node.
		/// </summary>
		public int WebsocketPort { get; set; }

		/// <summary>
		/// The Port for the Rest Server of this Node.
		/// </summary>
		public int RestPort { get; set; }

		/// <summary>
		/// The Region this node is hosted in.
		/// </summary>
		public Region Region { get; set; }
    }
}
