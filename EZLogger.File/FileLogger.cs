using System;
using System.Text;
using EZLogger;

namespace EZLogger.File
{
    public class FileLogger : ILogger
    {
        private readonly IFileWriter _FileWriter;

        public FileLogger(IFileWriter fileWriter)
        {
            _FileWriter = fileWriter;
        }

        public void LogMessage(string message)
        {
            string logMessage = FormatMessage(LogLevel.Info, message);
            WriteMessage(logMessage);
        }

        public void LogMessage(string message, LogLevel level)
        {
            string logMessage = FormatMessage(level, message);
            WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex)
        {
            string exceptionMessage = GetExceptionDetails(ex);
            string logMessage = FormatMessage(LogLevel.Error, exceptionMessage);
            WriteMessage(logMessage);
        }

        public void LogMessage(Exception ex, LogLevel level)
        {
            string exceptionMessage = GetExceptionDetails(ex);
            string logMessage = FormatMessage(level, exceptionMessage);
            WriteMessage(logMessage);
        }

        private void WriteMessage(string content) 
        {
            _FileWriter.Write(content);
        }

        private string GetExceptionDetails(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(ex.Message).Append(ex.StackTrace)
                .Append(GetExceptionDetails(ex.InnerException));

            return sb.ToString();
        }

        private string FormatMessage(LogLevel logLevel, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongDateString())
                .Append(" ")
                .Append(DateTime.Now.ToLongTimeString())
                .Append(" - ")
                .Append(Enum.GetName(typeof(LogLevel), logLevel))
                .Append(" - ")
                .Append(message ?? string.Empty);

            return sb.ToString();
        }
    }
}
