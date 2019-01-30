using Microsoft.Extensions.DependencyInjection;

namespace EZLogger.File
{
    public static class FileLoggerExtensions
    {
        public static IServiceCollection AddFileLogger<TFormatter>(this IServiceCollection serviceCollection, string logFileName) 
            where TFormatter : class, IFormatter<string> => 
                serviceCollection.AddScoped<ILogger, Logger>()
                                 .AddSingleton<IWriter>(new FileWriter(logFileName))
                                 .AddSingleton<IFormatter<string>, TFormatter>();

        public static IServiceCollection AddFileLogger(this IServiceCollection serviceCollection, string logFileName) => 
            AddFileLogger<DefaultFileMessageFormatter>(serviceCollection, logFileName);
    }
}