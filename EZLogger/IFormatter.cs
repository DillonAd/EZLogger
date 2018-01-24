using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public interface IFormatter
    {
        string GetExceptionDetails(Exception ex);
        string FormatMessage(LogLevel logLevel, string message);
    }
}
