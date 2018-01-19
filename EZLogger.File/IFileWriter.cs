using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.File
{
    public interface IFileWriter
    {
        void Write(string content);
    }
}
