using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public class Formatter : IFormatter
    {
        public string GetExceptionDetails(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(ex.Message).Append(ex.StackTrace)
                .Append(GetExceptionDetails(ex.InnerException));

            return sb.ToString();
        }

        public string FormatMessage(LogLevel logLevel, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongDateString())
                .Append(" ")
                .Append(DateTime.Now.ToLongTimeString())
                .Append(" - ")
                .Append(Enum.GetName(typeof(LogLevel), logLevel))
                .Append(" - ")
                .Append(message ?? string.Empty);

            return sb.ToString();
        }
    }
}
