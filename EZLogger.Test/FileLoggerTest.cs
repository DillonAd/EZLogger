using EZLogger;
using EZLogger.File;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EZLogger.Test
{
    public class FileLoggerTest
    {

        [Fact]
        public void LogString()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            string message = "test message";

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(message);

            Assert.Collection(writtenMessages, item => Assert.Contains(message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), LogLevel.Info), item));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogStringWithLevel()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            string message = "test message";
            LogLevel level = LogLevel.Debug;

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(message, level);

            Assert.Collection(writtenMessages, item => Assert.Contains(message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), level), item));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogException()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            Exception ex = GetValidException("test exception");

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(ex);

            Assert.Collection(writtenMessages, item => Assert.Contains(ex.Message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(ex.StackTrace, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), LogLevel.Error), item));
            Assert.Single(writtenMessages);
        }

        [Fact]
        public void LogExceptionWithLevel()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            Exception ex = GetValidException("test exception");

            LogLevel level = LogLevel.Warning;

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(ex, level);

            Assert.Collection(writtenMessages, item => Assert.Contains(ex.Message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(ex.StackTrace, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), level), item));
            Assert.Single(writtenMessages);
        }



        [Fact]
        public void LogMessage_NullString()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            string message = null;

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(message);

            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), LogLevel.Info), item));
        }

        [Fact]
        public void LogException_NestedException()
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            Exception innerEx = GetValidException("test inner exception");
            Exception ex = GetValidException("test exception", innerEx);
            
            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(ex);

            Assert.Collection(writtenMessages, item => Assert.Contains(ex.Message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(ex.StackTrace, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(innerEx.Message, item));
            Assert.Collection(writtenMessages, item => Assert.Contains(innerEx.StackTrace, item));
            Assert.Single(writtenMessages);
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void LogMessage_AllLevels(LogLevel level)
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            string message = "test message";

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(message, level);

            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), level), item));
        }

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        public void LogException_AllLevels(LogLevel level)
        {
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            Exception ex = GetValidException("test message");

            ILogger logger = new FileLogger(mockObj.Object);
            logger.LogMessage(ex, level);

            Assert.Collection(writtenMessages, item => Assert.Contains(Enum.GetName(typeof(LogLevel), level), item));
        }

        [Fact]
        public void LogMessage_MultipleMessages()
        {
            int messageCount = 100;
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            ILogger logger = new FileLogger(mockObj.Object);

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
            List<string> writtenMessages = new List<string>();

            var mockObj = new Mock<IFileWriter>();
            mockObj.Setup(x => x.Write(It.IsAny<string>()))
                .Callback((string content) => writtenMessages.Add(content));

            Exception ex;

            ILogger logger = new FileLogger(mockObj.Object);
            
            for(int i = 0; i < 100; i++)
            {
                ex = GetValidException("test message " + i);
                logger.LogMessage(ex);
            }

            Assert.Equal(messageCount, writtenMessages.Count);
        }

        private Exception GetValidException(string message, Exception innerException = null)
        {
            try
            {
                throw new Exception(message, innerException);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }
    }
}
