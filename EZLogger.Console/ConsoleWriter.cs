using System;
using System.IO;

namespace EZLogger.Console
{
    public class ConsoleWriter : IWriter
    {
        private readonly IFormatter<string> _formatter;
        private readonly TextWriter _textWriter;

        public ConsoleWriter(IFormatter<string> formatter)
        {
            _formatter = formatter;
            _textWriter = System.Console.Out;
        }

        public void WriteMessage(LogMessage message)
        {
            string content = _formatter.FormatMessage(message);
            _textWriter.WriteLine(content);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _textWriter.Flush();
            }
        }
    }
}
