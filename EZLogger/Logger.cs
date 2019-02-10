using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EZLogger
{
    public class Logger : ILogger
    {
        private readonly IWriter _writer;
        private readonly ConcurrentQueue<LogMessage> _messages;
        private event LogMessageHandler _logMessage;
            
        public Logger(IWriter writer)
        {
            _writer = writer;
            _messages = new ConcurrentQueue<LogMessage>();
            _logMessage += PersistMessage;
        }

        public void LogMessage(string message, LogLevel level)
        {
            _messages.Enqueue(new LogMessage(level, message));
            _logMessage();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _writer.Dispose();
            }
        }

        private void PersistMessage()
        {
            lock(_writer)
            {
                if(_messages.TryDequeue(out var message))
                {
                    _writer.WriteMessage(message);
                }
            }
        }
    }
}
