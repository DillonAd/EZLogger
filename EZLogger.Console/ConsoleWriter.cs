using System;
using System.IO;
using System.Threading.Tasks;

namespace EZLogger.Console
{
    public class ConsoleWriter : IWriter
    {
        private readonly IFormatter<string> _formatter;
        private readonly ConsoleColor _defaultForegroundColor;
        private readonly ConsoleColor _defaultBackgroundColor;

        public ConsoleWriter(IFormatter<string> formatter)
        {
            _formatter = formatter;

            _defaultForegroundColor = System.Console.ForegroundColor;
            _defaultBackgroundColor = System.Console.BackgroundColor;
        }

        public void WriteMessage(LogMessage message)
        {
            string content = _formatter.FormatMessage(message);
            var consoleColors = GetConsoleColors(message.Level);

            var levelStr = Enum.GetName(typeof(LogLevel), message.Level);
            var levelIdx = content.IndexOf(levelStr);

            if(levelIdx < 0)
            {
                System.Console.WriteLine(content);
            }
            else
            {
                System.Console.WriteLine(content.Substring(0, levelIdx));
                
                System.Console.ForegroundColor = consoleColors.ForegroundColor;
                System.Console.BackgroundColor = consoleColors.BackgroundColor;
                
                System.Console.WriteLine(levelStr);

                System.Console.ForegroundColor = _defaultForegroundColor;
                System.Console.BackgroundColor = _defaultBackgroundColor;

                var part3Idx = levelIdx + levelStr.Length;
                
                if(part3Idx < content.Length)
                {
                    System.Console.WriteLine(content.Substring(part3Idx));
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(disposing)
            {

            }
        }

        private (ConsoleColor ForegroundColor, ConsoleColor BackgroundColor) GetConsoleColors(LogLevel loglevel)
        {
            switch(loglevel)
            {
                case LogLevel.Trace:
                    return (ConsoleColor.White, ConsoleColor.Gray);
                case LogLevel.Debug:
                    return (ConsoleColor.Black, ConsoleColor.Gray);
                case LogLevel.Info:
                    return (ConsoleColor.Black, ConsoleColor.Cyan);
                case LogLevel.Warning:
                    return (ConsoleColor.Black, ConsoleColor.Yellow);
                case LogLevel.Error:
                    return (ConsoleColor.Black, ConsoleColor.Red);
                case LogLevel.Critical:
                    return (ConsoleColor.White, ConsoleColor.Red);
                default:
                    return (_defaultForegroundColor, _defaultBackgroundColor);
            }   
        }
    }
}
