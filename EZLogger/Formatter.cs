using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public abstract class Formatter<T> : IFormatter<T>
    {
        public abstract T FormatMessage(LogMessage message);

        protected string GetExceptionDetails(Exception ex) =>
            ex == null ? string.Empty : $"{ex.Message}\n{ex.StackTrace}\n\n{GetExceptionDetails(ex.InnerException)}";
    }
}
