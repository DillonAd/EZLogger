using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public class Logger : ILogger
    {
        private readonly IWriter _Writer;

        public Logger(IWriter writer)
        {
            _Writer = writer;
        }

        public void LogMessage(string message)
        {
            LogMessage logMessage = new LogMessage(LogLevel.Info, message);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(string message, LogLevel level)
        {
            LogMessage logMessage = new LogMessage(level, message);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex)
        {
            LogMessage logMessage = new LogMessage(LogLevel.Error, ex);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex, LogLevel level)
        {
            LogMessage logMessage = new LogMessage(level, ex);
            _Writer.WriteMessage(logMessage);
        }
    }
}
