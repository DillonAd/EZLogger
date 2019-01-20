using EZLogger;
using System;
using System.Text;

namespace EZLogger.Console
{
    public class DefaultConsoleMessageFormatter : Formatter<string>
    {
        public override string FormatMessage(LogMessage message)
        {
            var sb = new StringBuilder();

            if (message != null)
            {
                sb.Append(message.Date.ToLongDateString()).Append(" ")
                    .Append(message.Date.ToLongTimeString()).Append(" - ")
                    .Append(Enum.GetName(typeof(LogLevel), message.Level)).Append(" - ")
                    .Append(message.Message).Append(Environment.NewLine)
                    .Append(GetExceptionDetails(message.Exception))
                    .Append(Environment.NewLine).Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
