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
        public void LogString()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = "test message";

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(msg);

            Assert.Collection(writtenMessages, item => Assert.Equal(msg, item.Message));
            Assert.Collection(writtenMessages, item => Assert.Equal(LogLevel.Info, item.Level));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogStringWithLevel()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = "test message";
            LogLevel level = LogLevel.Debug;

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(msg, level);

            Assert.Collection(writtenMessages, item => Assert.Equal(msg, item.Message));
            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogException()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            Exception ex = TestExceptionGenerator.GetValidException("test exception");

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(ex);

            Assert.Collection(writtenMessages, item => Assert.Equal(ex, item.Exception));
            Assert.Collection(writtenMessages, item => Assert.Equal(LogLevel.Error, item.Level));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogExceptionWithLevel()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            Exception ex = TestExceptionGenerator.GetValidException("test exception");

            LogLevel level = LogLevel.Warning;

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(ex, level);

            Assert.Collection(writtenMessages, item => Assert.Equal(ex, item.Exception));
            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
            Assert.Single(writtenMessages);
        }



        [Fact]
        public void LogMessage_NullString()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = null;

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(msg);

            Assert.Collection(writtenMessages, item => Assert.Equal(LogLevel.Info, item.Level));
        }

        [Fact]
        public void LogException_NestedException()
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            Exception innerEx = TestExceptionGenerator.GetValidException("test inner exception");
            Exception ex = TestExceptionGenerator.GetValidException("test exception", innerEx);

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(ex);

            Assert.Collection(writtenMessages, item => Assert.Equal(ex, item.Exception));
            Assert.Single(writtenMessages);
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void LogMessage_AllLevels(LogLevel level)
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            string msg = "test message";

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(msg, level);

            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void LogException_AllLevels(LogLevel level)
        {
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            Exception ex = TestExceptionGenerator.GetValidException("test message");

            ILogger logger = new Logger(mockObj.Object);
            logger.LogMessage(ex, level);

            Assert.Collection(writtenMessages, item => Assert.Equal(level, item.Level));
        }

        [Fact]
        public void LogMessage_MultipleMessages()
        {
            int messageCount = 100;
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            ILogger logger = new Logger(mockObj.Object);

            for (int i = 0; i < 100; i++)
            {
                logger.LogMessage("test message " + i);
            }

            Assert.Equal(messageCount, writtenMessages.Count);
        }

        [Fact]
        public void LogException_MultipleMessages()
        {
            int messageCount = 100;
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            Exception ex;

            ILogger logger = new Logger(mockObj.Object);

            for (int i = 0; i < 100; i++)
            {
                ex = TestExceptionGenerator.GetValidException("test message " + i);
                logger.LogMessage(ex);
            }

            Assert.Equal(messageCount, writtenMessages.Count);
        }
    }
}