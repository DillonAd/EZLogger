using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public abstract class Formatter<T> : IFormatter<T>
    {
        protected readonly Dictionary<LogLevel, string> LogLevels;

        protected Formatter()
        {
            if(LogLevels == null)
            {
                LogLevels = new Dictionary<LogLevel, string>();

                foreach(var level in Enum.GetValues(typeof(LogLevel)))
                {
                    LogLevels.Add((LogLevel)level, Enum.GetName(typeof(LogLevel), level));
                }
            }
        }

        public abstract T FormatMessage(LogMessage message);

        protected string GetExceptionDetails(Exception ex) =>
            ex == null ? string.Empty : $"{ex.Message}\n{ex.StackTrace}\n\n{GetExceptionDetails(ex.InnerException)}";
    }
}
