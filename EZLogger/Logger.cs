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
        private readonly Task _processor;
        private bool _disposing { get; set; }
        
        public Logger(IWriter writer)
        {
            _disposing = false;
            _writer = writer;
            _messages = new ConcurrentQueue<LogMessage>();
            _processor = Task.Run(() => PersistMessages());
        }

        public void LogMessage(string message, LogLevel level) =>
            _messages.Enqueue(new LogMessage(level, message));

        public void LogMessage(Exception ex, LogLevel level) =>
            _messages.Enqueue(new LogMessage(level, ex));

        public void Critical(string message) =>
            LogMessage(message, LogLevel.Critical);

        public void Critical(Exception ex) =>
            LogMessage(ex, LogLevel.Critical);

        public void Debug(string message) =>
            LogMessage(message, LogLevel.Debug);

        public void Debug(Exception ex) =>
            LogMessage(ex, LogLevel.Debug);

        public void Error(string message) =>
            LogMessage(message, LogLevel.Error);

        public void Error(Exception ex) =>
            LogMessage(ex, LogLevel.Error);

        public void Info(string message) =>
            LogMessage(message, LogLevel.Info);

        public void Info(Exception ex) =>
            LogMessage(ex, LogLevel.Info);

        public void Trace(string message) =>
            LogMessage(message, LogLevel.Trace);

        public void Trace(Exception ex) =>
            LogMessage(ex, LogLevel.Trace);

        public void Warning(string message) =>
            LogMessage(message, LogLevel.Warning);

        public void Warning(Exception ex) =>
            LogMessage(ex, LogLevel.Warning);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _disposing = true;
                _processor.Wait(10000);
                _processor.Dispose();
                _writer.Dispose();
            }
        }

        private void PersistMessages()
        {
            LogMessage message;

            while(!_disposing || !_messages.IsEmpty)
            {
                if(_messages.TryDequeue(out message))
                {
                    _writer.WriteMessage(message);        
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
