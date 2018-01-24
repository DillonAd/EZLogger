using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public abstract class Formatter<T> : IFormatter<T>
    {
        public abstract T FormatMessage(LogMessage message);

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
    }
}
