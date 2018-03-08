using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public class LogMessage
    {
        public LogLevel Level { get; }
        public DateTime Date { get; }
        public Exception Exception { get; }
        public string Message { get; }

        public LogMessage(LogLevel level, string message)
        {
            Level = level;
            Date = DateTime.Now;
            Message = message;
        }

        public LogMessage(LogLevel level, Exception ex)
        {
            Level = level;
            Date = DateTime.Now;
            Message = string.Empty;
            Exception = ex;
        }
    }
}
