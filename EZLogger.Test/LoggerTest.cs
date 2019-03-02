using EZLogger.Test.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.Single(writtenMessages);
            Assert.Equal(msg, writtenMessages[0].Message);
            Assert.Equal(level, writtenMessages[0].Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void LogMessageWithLevel()
        {
            // Assemble
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                .Callback((LogMessage message) => writtenMessages.Add(message));

            var ex = new Exception("test message");
            LogLevel level = LogLevel.Debug;

            // Act
            using(ILogger logger = new Logger(mockObj.Object))
            {
                logger.LogMessage(ex, level);
            }

            // Assert
            Assert.Single(writtenMessages);
            Assert.Contains(ex.Message, writtenMessages[0].Exception.Message);
            Assert.Equal(level, writtenMessages[0].Level);
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

        [Theory]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Info)]
        [InlineData(LogLevel.Warning)]
        [Trait("Category", "unit")]
        public void LogException_AllLevels(LogLevel level)
        {
            // Assemble
            List<LogMessage> writtenMessages = new List<LogMessage>();

            var mockObj = new Mock<IWriter>();
            mockObj.Setup(x => x.WriteMessage(It.IsAny<LogMessage>()))
                   .Callback((LogMessage message) => writtenMessages.Add(message));

            var ex = new Exception("test message");
            
            // Act
            using(ILogger logger = new Logger(mockObj.Object))
            {
                logger.LogMessage(ex, level);
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
        public void Log_All_Exceptions()
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
                logger.LogMessage(new Exception(i.ToString()), LogLevel.Info);
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
        public void Log_All_Exceptions_Text()
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
                logger.LogMessage(new Exception(i.ToString()), LogLevel.Info);
            }

            // Act
            logger.Dispose();

            // Assert
            Assert.All(messages, m => Assert.False(string.IsNullOrWhiteSpace(m.Exception.Message)));
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

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Exceptions_With_Timeout()
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
                logger.LogMessage(new Exception(i.ToString()), LogLevel.Info);
            }

            // Act
            logger.Dispose();

            // Assert
            Assert.NotEmpty(messages);
            Assert.InRange(messages.Count, 0, messageCount);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Critical()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Critical("test");
            }
            

            // Assert
            Assert.Equal(LogLevel.Critical, messages.First().Level);
        }
        
        [Fact]
        [Trait("Category", "unit")]
        public void Log_Critical_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Critical)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Critical(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Debug()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Debug("test");
            }
            
            // Assert
            Assert.Equal(LogLevel.Debug, messages.First().Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Debug_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Debug)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Debug(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Error()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Error("test");
            }
            
            // Assert
            Assert.Equal(LogLevel.Error, messages.First().Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Error_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Error)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Error(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Info()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Info("test");
            }
            
            // Assert
            Assert.Equal(LogLevel.Info, messages.First().Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Info_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Info)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Info(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Trace()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Trace("test");
            }
            
            // Assert
            Assert.Equal(LogLevel.Trace, messages.First().Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Trace_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Trace)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Trace(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Warning()
        {
            // Assemble
            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.IsAny<LogMessage>()))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Warning("test");
            }
            
            // Assert
            Assert.Equal(LogLevel.Warning, messages.First().Level);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Log_Warning_Message()
        {
            // Assemble
            const string messageInput = "His name was Robert Paulson.";

            var messages = new List<LogMessage>();
            var writer = new Mock<IWriter>();
            writer.Setup(w => w.WriteMessage(It.Is<LogMessage>(l => l.Level == LogLevel.Warning)))
                  .Callback<LogMessage>(m => messages.Add(m));
            
            // Act
            using(var logger = new Logger(writer.Object))
            {
                logger.Warning(messageInput);
            }
            
            // Assert
            Assert.Equal(messageInput, messages.First().Message);
        }
    }
}