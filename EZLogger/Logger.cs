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

            while(!_disposing || _messages.Count > 0)
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
