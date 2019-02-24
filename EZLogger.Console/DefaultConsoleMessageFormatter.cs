using EZLogger;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.Console
{
    public class DefaultConsoleMessageFormatter : Formatter<string>
    {
        private static Dictionary<LogLevel, string> _logLevels;

        public DefaultConsoleMessageFormatter()
        {
            if(_logLevels == null)
            {
                _logLevels = new Dictionary<LogLevel, string>();

                foreach(var level in Enum.GetValues(typeof(LogLevel)))
                {
                    _logLevels.Add((LogLevel)level, Enum.GetName(typeof(LogLevel), level));
                }
            }
        }

        public override string FormatMessage(LogMessage message)
        {
            var sb = new StringBuilder();

            if (message != null)
            {
                sb.Append(message.Date.ToLongDateString()).Append(" ")
                    .Append(message.Date.ToLongTimeString()).Append(" - ")
                    .Append(_logLevels[message.Level]).Append(" - ")
                    .Append(message.Message)
                    .Append(GetExceptionDetails(message.Exception));
            }

            return sb.ToString();
        }
    }
}
