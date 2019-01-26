using System;

namespace EZLogger
{
    public interface IWriter : IDisposable
    {
        void WriteMessage(LogMessage message);
    }
}
