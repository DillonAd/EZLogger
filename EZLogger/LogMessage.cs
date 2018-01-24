using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public class LogMessage
    {
        public LogLevel Level { get; private set; }
        public DateTime Date { get; private set; }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }

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
