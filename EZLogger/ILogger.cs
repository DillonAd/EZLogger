using System;

namespace EZLogger
{
    public interface ILogger : IDisposable
    {
        void LogMessage(string message, LogLevel level);
    }
}