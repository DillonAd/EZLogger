using EZLogger.File;
using EZLogger.Test.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EZLogger.Test
{
    public class FileMessageFormatTests
    {
        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void FileMessageFormat_Message(LogLevel level)
        {
            string message = "test message";

            LogMessage msg = new LogMessage(level, message);
            IFormatter<string> formatter = new DefaultFileMessageFormatter();
            string result = formatter.FormatMessage(msg);

            Assert.Contains(Enum.GetName(typeof(LogLevel), level), result);
            Assert.Contains(message, result);
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void FileMessageFormat_Exception(LogLevel level)
        {
            string message = "test message";
            Exception ex = TestExceptionGenerator.GetValidException(message);

            LogMessage msg = new LogMessage(level, ex);
            IFormatter<string> formatter = new DefaultFileMessageFormatter();
            string result = formatter.FormatMessage(msg);

            Assert.Contains(Enum.GetName(typeof(LogLevel), level), result);
            Assert.Contains(ex.Message, result);
            Assert.Contains(ex.StackTrace, result);
        }

        [Fact]
        public void FileMessageFormat_NullLogMessage()
        {
            LogMessage msg = null;
            IFormatter<string> formatter = new DefaultFileMessageFormatter();
            string result = formatter.FormatMessage(msg);

            Assert.Empty(result);
        }
    }
}
