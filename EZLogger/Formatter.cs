using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger
{
    public abstract class Formatter<T> : IFormatter<T>
    {
        protected readonly Dictionary<LogLevel, string> LogLevels;

        public Formatter()
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

        // This method uses string.Format instead of $ interpolation
        // to be backwards compatible with previous versions of C#.
        // Once the dollar sign interpolation method is ubiquitous
        // this can be changed to be more concise.
        protected string GetExceptionDetails(Exception ex) =>
            ex == null ? string.Empty : $"{ex.Message}\n{ex.StackTrace}\n\n{GetExceptionDetails(ex.InnerException)}";
    }
}
