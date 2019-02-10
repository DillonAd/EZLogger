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
        [Trait("Category", "unit")]
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
            Assert.Equal(msg, writtenMessages[0].Message);
            Assert.Equal(level, writtenMessages[0].Level);
            Assert.Single(writtenMessages);
        }

        
        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        [Trait("Category", "unit")]
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
            Assert.Equal(level, writtenMessages[0].Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_All_Messages()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));

            var logger = new Logger(writer.Object);

            const int messageCount = 50;

            for(int i = 0; i < messageCount; i++)
            {
                logger.LogMessage(i.ToString(), LogLevel.Info);
            }

            // Act
            logger.Dispose();

            // Assert
            Assert.Equal(messageCount, messages.Count);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_All_Messages_Text()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));

            var logger = new Logger(writer.Object);

            const int messageCount = 50;

            for(int i = 0; i < messageCount; i++)
            {
                logger.LogMessage(i.ToString(), LogLevel.Info);
            }

            // Act
            logger.Dispose();

            // Assert
            Assert.All(messages, m => Assert.False(string.IsNullOrWhiteSpace(m.Message)));
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Messages_With_Timeout()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));

            var logger = new Logger(writer.Object);

            const int messageCount = 100000;

            for(int i = 0; i < messageCount; i++)
            {
                logger.LogMessage(i.ToString(), LogLevel.Info);
            }

            // Act
            logger.Dispose();

            // Assert
            Assert.NotEmpty(messages);
            Assert.InRange(messages.Count, 0, messageCount);
        }
    }
}