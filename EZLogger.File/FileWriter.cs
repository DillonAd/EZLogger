using System;
using System.IO;
using System.Threading.Tasks;

namespace EZLogger.File
{
    public class FileWriter : IDisposable, IWriter
    {
        private readonly StreamWriter _writer;
        private readonly IFormatter<string> _formatter;
        private readonly object _lockObj;

        public string FileName { get; }

        public FileWriter(string fileName) : this(new DefaultFileMessageFormatter(), fileName) { }

        public FileWriter(IFormatter<string> formatter, string fileName)
        {
            _formatter = formatter;
            _writer = new StreamWriter(fileName, true);
            _lockObj = new object();

            FileName = fileName;
        }

        public void WriteMessage(LogMessage message)
        {
            string content = _formatter.FormatMessage(message);
            lock(_writer)
            {
                _writer.Write(content);
            }
        }

        public async Task WriteMessageAsync(LogMessage message) =>
            await Task.Run(() => WriteMessage(message));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(disposing)
            {
                _writer.Dispose();
            }
        }
    }
}
