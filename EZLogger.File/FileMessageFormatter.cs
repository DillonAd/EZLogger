using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.File
{
    public class FileMessageFormatter : Formatter<string>
    {
        public override string FormatMessage(LogMessage message)
        {
            StringBuilder sb = new StringBuilder();

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
