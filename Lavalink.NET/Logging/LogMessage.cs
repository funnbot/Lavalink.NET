using System;
using System.Collections.Generic;
using System.Text;

namespace Lavalink.NET.Logging
{
    public class LogMessage
    {
		public string Source { get; }
		public string Message { get; }
		public Exception Exception { get; }
		public LogLevel LogLevel { get; }

		public LogMessage(LogLevel level, string content, string source, Exception execption = null)
		{
			LogLevel = level;
			Message = content;
			Source = source;
			Exception = execption;
		}
    }
}
