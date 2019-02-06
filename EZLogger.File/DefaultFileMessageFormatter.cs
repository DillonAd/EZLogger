using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.File
{
    public class DefaultFileMessageFormatter : Formatter<string>
    {
        public override string FormatMessage(LogMessage message)
        {
            if (message != null && !string.IsNullOrWhiteSpace(message.Message))
            {
                var sb = new StringBuilder();

                sb.Append(message.Date.ToLongDateString()).Append(" ")
                  .Append(message.Date.ToLongTimeString()).Append(" - ")
                  .Append(Enum.GetName(typeof(LogLevel), message.Level)).Append(" - ")
                  .Append(message.Message);
                
                if(message.Exception != null)
                {
                    sb.Append(Environment.NewLine)
                      .Append(GetExceptionDetails(message.Exception));
                }

                return sb.ToString();
            }
            else
            {
                return string.Empty;   
            }
        }
    }
}
