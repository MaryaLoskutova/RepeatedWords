using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Service;

namespace TextAnalyzer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>().Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWordsAnalyzingService, WordsAnalyzingService>();
            services.AddTransient<IFileAnalyzingService, FileAnalyzingService>();
            services.AddTransient<App>();
        }
    }
}