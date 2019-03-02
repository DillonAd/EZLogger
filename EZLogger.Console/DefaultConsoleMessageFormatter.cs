using EZLogger;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.Console
{
    public class DefaultConsoleMessageFormatter : Formatter<string>
    {
        public DefaultConsoleMessageFormatter() : base() { }

        public override string FormatMessage(LogMessage message)
        {
            var sb = new StringBuilder();

            if (message != null)
            {
                sb.Append(message.Date.ToLongDateString()).Append(" ")
                    .Append(message.Date.ToLongTimeString()).Append(" - ")
                    .Append(LogLevels[message.Level]).Append(" - ")
                    .Append(message.Message)
                    .Append(GetExceptionDetails(message.Exception));
            }

            return sb.ToString();
        }
    }
}
