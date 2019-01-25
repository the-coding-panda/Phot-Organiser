using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using PhotoOraganiser.Core;
using System.Linq;

namespace PhotoOrganiser
{
    public class Program
    {
        private static IFolderActions _iFolderActions;

        static void Main(string[] args)
        {
            //Initialise Services & Settings
            ConfigureServices(new ServiceCollection());
            var appSettings = ConfigureSettings();

            //Copy Files
            var files = _iFolderActions.CopyFolderContents(appSettings.OriginLocation, appSettings.DestinationLocation, true);
            if (files.Any())
            {
                Console.WriteLine("Lets organise these new files.");
                
                _iFolderActions.CreateFolders(files, appSettings.DestinationLocation);
                _iFolderActions.OrganiseFilesIntoFolders(appSettings.DestinationLocation);
            }

            Console.WriteLine("Hello World!");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //setup our DI
            var provider = serviceCollection
                .AddSingleton<IFolderActions, PhotoManagementActions>().BuildServiceProvider();

            _iFolderActions = provider.GetService<IFolderActions>();
        }

        private static AppSettings ConfigureSettings()
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            return configuration.GetSection("appSettings").Get<AppSettings>();
        }

    }
}
