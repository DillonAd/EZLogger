using Microsoft.Extensions.DependencyInjection;

namespace EZLogger
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddEZLogger<T>(this IServiceCollection serviceCollection)
            where T : class, IWriter =>        
                serviceCollection.AddSingleton<IWriter, T>()
                                .AddTransient<ILogger, Logger>();
    }
}