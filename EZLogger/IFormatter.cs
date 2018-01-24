using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public interface IFormatter<T>
    {
        string GetExceptionDetails(Exception ex);
        T FormatMessage(LogMessage message);
    }
}
