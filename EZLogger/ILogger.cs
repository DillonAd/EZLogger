using System;

namespace EZLogger
{
    public interface ILogger : IDisposable
    {
        void LogMessage(string message, LogLevel level);
        void LogMessage(Exception ex, LogLevel level);
        void Critical(string message);
        void Critical(Exception ex);
        void Debug(string message);
        void Debug(Exception ex);
        void Error(string message);
        void Error(Exception ex);
        void Info(string message);
        void Info(Exception ex);
        void Trace(string message);
        void Trace(Exception ex);
        void Warning(string message);
        void Warning(Exception ex);
    }
}