﻿using System;

namespace EZLogger.File
{
    public class FileWriter : IWriter
    {
        private readonly IFormatter<string> _Formatter;
        private readonly object _LockObj;
        
        public string FileName { get; private set; }

        public FileWriter(IFormatter<string> formatter, string fileName)
        {
            _Formatter = formatter;
            _LockObj = new object();

            FileName = fileName;
        }

        public void WriteMessage(LogMessage message)
        {
            string content = _Formatter.FormatMessage(message);
            lock(_LockObj)
            {
                System.IO.File.AppendAllText(FileName, content);
            }
        }
    }
}
