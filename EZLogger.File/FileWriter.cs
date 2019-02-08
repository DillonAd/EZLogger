using System;
using System.IO;
using System.Threading.Tasks;

namespace EZLogger.File
{
    public class FileWriter : IWriter
    {
        private readonly StreamWriter _writer;
        private readonly IFormatter<string> _formatter;

        public string FileName { get; }

        public FileWriter(string fileName) : this(new DefaultFileMessageFormatter(), fileName) { }

        public FileWriter(IFormatter<string> formatter, string fileName)
        {
            _formatter = formatter;
            _writer = new StreamWriter(fileName, true);

            FileName = fileName;
        }

        public void WriteMessage(LogMessage message)
        {
            string content = _formatter.FormatMessage(message);
            _writer.WriteLine(content);
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
                _writer.Flush();
                _writer.Dispose();
            }
        }
    }
}
