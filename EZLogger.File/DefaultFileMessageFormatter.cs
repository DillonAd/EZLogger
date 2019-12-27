using System;
using System.Text;

namespace EZLogger.File
{
    public class DefaultFileMessageFormatter : Formatter<string>
    {
        public override string FormatMessage(LogMessage message)
        {
            var sb = new StringBuilder();

            if (message != null)
            {
                sb.Append(message.Date.ToLongDateString()).Append(" ")
                  .Append(message.Date.ToLongTimeString()).Append(" - ")
                  .Append(LogLevels[message.Level]).Append(" - ");

                if(!string.IsNullOrWhiteSpace(message.Message))
                {
                    sb.Append(message.Message);
                }

                if(message.Exception != null)
                {
                    sb.Append(Environment.NewLine)
                        .Append(GetExceptionDetails(message.Exception));
                }
            }
            
            return sb.ToString();
        }
    }
}
