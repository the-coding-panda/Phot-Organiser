using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace PhotoOrganiser
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            //Initialise
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            //Point to a origin location
            //Check for new files
            //If present, copy to destination location
            //If year folder present, open (if not create)
            //If month folder present, open (if not create)
            //Copy file(s) to folder
            //Remove files from origin location when complete
            Console.WriteLine("Hello World!");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

    }
    }
