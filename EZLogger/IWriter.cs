using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public interface IWriter
    {
        void WriteMessage(string content);
    }
}
