using Microsoft.Extensions.DependencyInjection;

namespace EZLogger.File
{
    public static class FileLoggerExtensions
    {
        public static IServiceCollection AddFileLogger<TFormatter>(this IServiceCollection serviceCollection) 
            where TFormatter : IFormatter<string> => 
                serviceCollection.AddScoped<ILogger, Logger>()
                                 .AddSingleton<IWriter, FileWriter>()
                                 .AddSingleton<IFormatter<string>, DefaultFileMessageFormatter>();

        public static IServiceCollection AddFileLogger(this IServiceCollection serviceCollection) => 
            AddFileLogger<DefaultFileMessageFormatter>(serviceCollection);
    }
}