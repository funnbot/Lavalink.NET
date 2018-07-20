using System;
using System.Collections.Generic;
using System.Text;

namespace Lavalink.NET.Logging
{
	/// <summary>
	/// Different levels for logs.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Shows from Debug to Error all logs. usefull in developement.
		/// </summary>
		Debug,
		/// <summary>
		/// Shows from Info to Error all logs. usefull in production.
		/// </summary>
		Info,
		/// <summary>
		/// Shows Warnings and Errors. usefull in production.
		/// </summary>
		Warning,
		/// <summary>
		/// Only shows Errors. usefull if you only want critical information.
		/// </summary>
		Error
	}
}
