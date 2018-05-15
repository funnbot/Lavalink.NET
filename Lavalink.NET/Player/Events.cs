﻿using System;
using Newtonsoft.Json;

namespace Lavalink.NET.Player
{
	/// <summary>
	/// Base TrackEventArgs class, all event args extend this class.
	/// </summary>
    public class TrackEventArgs : EventArgs
	{
		[JsonProperty("op")]
		public string OP { get; set; }
		[JsonProperty("track")]
		public string Track { get; set; }
		[JsonProperty("guildId")]
		public string GuildID { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }

		public TrackEventArgs(string op, string track, string guildID, string type)
		{
			OP = op ?? throw new ArgumentNullException(nameof(op));
			Track = track ?? throw new ArgumentNullException(nameof(track));
			GuildID = guildID ?? throw new ArgumentNullException(nameof(guildID));
			Type = type ?? throw new ArgumentNullException(nameof(type));
		}
	}
	
	/// <summary>
	/// TrackArgs for the TrackEnd event.
	/// </summary>
	public class TrackEndEventArgs : TrackEventArgs
	{
		[JsonProperty("reason")]
		public string Reason { get; set; }

		public TrackEndEventArgs(string op, string track, string guildID, string type, string reason)
			: base(op, track, guildID, type)
		{
			Reason = reason ?? throw new ArgumentNullException(nameof(reason));
		}
	}

	/// <summary>
	/// TrackArgs for the TrackExeption event.
	/// </summary>
	public class TrackExceptionEventArgs : TrackEventArgs
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		public TrackExceptionEventArgs(string op, string track, string guildID, string type, string error)
			: base(op, track, guildID, type)
		{
			Error = error ?? throw new ArgumentNullException(nameof(error));
		}
	}

	/// <summary>
	/// TrackArgs for the TrackStuck event.
	/// </summary>
	public class TrackStuckEventArgs : TrackEventArgs
	{
		[JsonProperty("thresholdMs")]
		public long ThresholdMS { get; set; }

		public TrackStuckEventArgs(string op, string track, string guildID, string type, string thresholdMS)
			: base(op, track, guildID, type)
		{
			ThresholdMS = Convert.ToInt64(thresholdMS ?? throw new ArgumentNullException(nameof(thresholdMS)) );
		}
	}

	public delegate void TrackEndEvent(object sender, TrackEndEventArgs e);
	public delegate void TrackExceptionEvent(object sender, TrackExceptionEventArgs e);
	public delegate void TrackStuckEvent(object sender, TrackStuckEventArgs e);
}
