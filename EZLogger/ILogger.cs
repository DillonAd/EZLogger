using System;

namespace EZLogger
{
    public interface ILogger : IDisposable
    {
        void LogMessage(string message, LogLevel level);
        void Critical(string message);
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Trace(string message);
        void Warning(string message);
    }
}