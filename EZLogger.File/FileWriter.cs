using System;

namespace EZLogger.File
{
    public class FileWriter : IWriter
    {
        private readonly object lockObj;
        
        public string FileName { get; private set; }

        public FileWriter(string fileName)
        {
            FileName = fileName;
            lockObj = new object();
        }

        public void WriteMessage(string content)
        {
            lock(lockObj)
            {
                System.IO.File.AppendAllText(FileName, content + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
