using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public class Logger : ILogger
    {
        private readonly IFormatter _Formatter;
        private readonly IWriter _Writer;

        public Logger(IFormatter formatter, IWriter writer)
        {
            _Formatter = formatter;
            _Writer = writer;
        }

        public void LogMessage(string message)
        {
            string logMessage = _Formatter.FormatMessage(LogLevel.Info, message);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(string message, LogLevel level)
        {
            string logMessage = _Formatter.FormatMessage(level, message);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex)
        {
            string exceptionMessage = _Formatter.GetExceptionDetails(ex);
            string logMessage = _Formatter.FormatMessage(LogLevel.Error, exceptionMessage);
            _Writer.WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex, LogLevel level)
        {
            string exceptionMessage = _Formatter.GetExceptionDetails(ex);
            string logMessage = _Formatter.FormatMessage(level, exceptionMessage);
            _Writer.WriteMessage(logMessage);
        }
    }
}
