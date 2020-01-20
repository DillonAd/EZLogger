using EZLogger.Console;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EZLogger.Test
{
    public class ConsoleLoggerExtensionsTests
    {
        [Fact]
        public void AddsConsoleWriter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddConsoleLogger();

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(ConsoleWriter));
        }

        [Fact]
        public void AddsDefaultFormatter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddConsoleLogger();

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(DefaultConsoleMessageFormatter));
        }

        [Fact]
        public void AddsCustomFormatter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddConsoleLogger<TestMessageFormatter>();

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(TestMessageFormatter));
        }
    }
}