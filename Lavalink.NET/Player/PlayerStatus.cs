using System;
using System.Collections.Generic;
using System.Text;

namespace Lavalink.NET.Player
{
	/// <summary>
	/// PlayerStatus for LavalinkPlayers.
	/// </summary>
	public enum PlayerStatus
	{
		/// <summary>
		/// Player was Instantiated.
		/// </summary>
		INSTANTIATED,

		/// <summary>
		/// Player is currently playing a Song.
		/// </summary>
		PLAYING,

		/// <summary>
		/// Player is currently paused.
		/// </summary>
		PAUSED,

		/// <summary>
		/// Player has ended playing a Song.
		/// </summary>
		ENDED,

		/// <summary>
		/// Player encountered an error while playing a Song.
		/// </summary>
		ERRORED,

		/// <summary>
		/// Player got stuck while playing.
		/// </summary>
		STUCK
	}
}
