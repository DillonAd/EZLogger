using System;

namespace EZLogger
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogMessage(string message, LogLevel level);
        void LogMessage(Exception ex);
        void LogMessage(Exception ex, LogLevel level);
    }
}