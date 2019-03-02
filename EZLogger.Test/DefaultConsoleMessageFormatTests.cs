using EZLogger.Console;
using EZLogger.Test.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EZLogger.Test
{
    public class DefaultConsoleMessageFormatTests
    {
        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        [Trait("Category", "unit")]
        public void ConsoleMessageFormat_Level(LogLevel level)
        {
            // Assemble
            string message = "test message";

            LogMessage msg = new LogMessage(level, message);
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);

            // Assert
            Assert.Contains(Enum.GetName(typeof(LogLevel), level), result);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ConsoleMessageFormat_Message()
        {
            // Assemble
            string message = "test message";

            LogMessage msg = new LogMessage(LogLevel.Critical, message);
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);
            
            // Assert
            Assert.Contains(message, result);
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        [Trait("Category", "unit")]
        public void ConsoleMessageFormat_Exception_Level(LogLevel level)
        {
            // Assemble
            string message = "test message";
            Exception ex = TestExceptionGenerator.GetValidException(message);

            LogMessage msg = new LogMessage(level, ex);
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);

            // Assert
            Assert.Contains(Enum.GetName(typeof(LogLevel), level), result);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ConsoleMessageFormat_Exception_Message()
        {
            // Assemble
            string message = "test message";
            Exception ex = TestExceptionGenerator.GetValidException(message);

            LogMessage msg = new LogMessage(LogLevel.Info, ex);
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);

            // Assert
            Assert.Contains(ex.Message, result);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ConsoleMessageFormat_Exception_StackTrace()
        {
            // Assemble
            string message = "test message";
            Exception ex = TestExceptionGenerator.GetValidException(message);

            LogMessage msg = new LogMessage(LogLevel.Debug, ex);
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);

            // Assert
            Assert.Contains(ex.StackTrace, result);
        }

        [Fact]
        public void ConsoleMessageFormat_NullLogMessage()
        {
            // Assemble
            LogMessage msg = null;
            IFormatter<string> formatter = new DefaultConsoleMessageFormatter();
            
            // Act
            string result = formatter.FormatMessage(msg);

            // Assert
            Assert.Empty(result);
        }
    }
}
