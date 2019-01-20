using EZLogger.Test.Utility;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EZLogger.Test
{
    public class LoggerTest
    {
        [Fact]
        public void LogStringWithLevel()
        {
            // Assemble
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = "test message";
            LogLevel level = LogLevel.Debug;

            // Act
            using(ILogger logger = new Logger(mockObj.Object))
            {
                logger.LogMessage(msg, level);
            }

            // Assert
            Assert.Collection(writtenMessages, item => Assert.Equal(msg, item.Message));
            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
            Assert.Single(writtenMessages);
        }

        
        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void LogMessage_AllLevels(LogLevel level)
        {
            // Assemble
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = "test message";
            
            // Act
            using(ILogger logger = new Logger(mockObj.Object))
            {
                logger.LogMessage(msg, level);
            }

            // Assert
            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
        }
    }
}