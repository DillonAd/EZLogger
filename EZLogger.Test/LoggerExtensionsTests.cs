using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EZLogger.Test
{
    public class LoggerExtensionTests
    {
        [Fact]
        public void AddsLogger()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddLogger<TestMessageFormatter>();

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(Logger));
        }

        [Fact]
        public void AddsFormatter()
        {
            // Assemble
            var sc = new ServiceCollection();

            // Act
            sc.AddLogger<TestMessageFormatter>();

            // Assert
            Assert.Contains(sc, s => s.ImplementationType == typeof(TestMessageFormatter));
        }
    }
}