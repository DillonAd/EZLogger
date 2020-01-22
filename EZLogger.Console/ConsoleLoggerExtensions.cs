using Microsoft.Extensions.DependencyInjection;

namespace EZLogger.Console
{
    public static class ConsoleLoggerExtensions
    {
        public static IServiceCollection AddConsoleLogger<TFormatter>(this IServiceCollection serviceCollection) 
            where TFormatter : class, IFormatter<string> => 
                serviceCollection.AddLogger<TFormatter>()
                                 .AddSingleton<IWriter, ConsoleWriter>();

        public static IServiceCollection AddConsoleLogger(this IServiceCollection serviceCollection) => 
            AddConsoleLogger<DefaultConsoleMessageFormatter>(serviceCollection);
    }
}