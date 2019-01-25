using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using PhotoOraganiser.Core.Interfaces;
using PhotoOraganiser.Core.Services;

namespace PhotoOrganiser
{
    class Program
    {
        public static IConfigurationRoot configuration;
        private IFolderActions iFolderActions;

        static void Main(string[] args)
        {
            //Initialise
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            
            Console.WriteLine("Hello World!, here is a change");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //setup our DI
            serviceCollection.AddSingleton<IFolderActions, PhotoManagementActions>().BuildServiceProvider();

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

    }
    }
