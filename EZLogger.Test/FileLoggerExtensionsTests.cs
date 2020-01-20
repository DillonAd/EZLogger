using EZLogger.File;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EZLogger.Test
{
    public class FileLoggerExtensionsTests
    {
        [Fact]
        public void AddsFileWriter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddFileLogger("log.log");

            // Assert
            Assert.Contains(sc, s => s.ImplementationInstance?.GetType() == typeof(FileWriter));
        }

        [Fact]
        public void AddsDefaultFormatter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddFileLogger("log.log");

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(DefaultFileMessageFormatter));
        }

        [Fact]
        public void AddsCustomFormatter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddFileLogger<TestMessageFormatter>("log.log");

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(TestMessageFormatter));
        }
    }
}