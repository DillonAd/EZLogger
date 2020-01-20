using Microsoft.Extensions.DependencyInjection;

namespace EZLogger
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddLogger<TFormatter>(this IServiceCollection serviceCollection)
            where TFormatter : class, IFormatter<string> => 
                serviceCollection.AddScoped<ILogger, Logger>()
                                 .AddSingleton<IFormatter<string>, TFormatter>();
    }
}