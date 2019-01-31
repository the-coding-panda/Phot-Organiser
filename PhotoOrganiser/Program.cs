using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using PhotoOraganiser.Core;
using Serilog;

namespace PhotoOrganiser
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Initialise Services & Settings
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<ProgramWorkflow>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(".\\logs\\logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            serviceCollection.AddLogging();
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // add services
            serviceCollection.AddTransient<IFolderActions, PhotoManagementActions>();

            // add app
            serviceCollection.AddTransient<ProgramWorkflow>();

        }


    }
}
