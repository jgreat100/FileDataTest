using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureDI();

            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect number of arguments specified, please supply an action and a file path");
                return;
            }

            var sizeActions = new List<string> { "-s", "--s", "/s", "--size" };
            var versionActions = new List<string> { "-v", "--v", "/v", "--version" };

            var action = args[0];
            var filePath = args[1];
            var fileDetailsService = serviceProvider.GetService<IFileDetailsService>();

            if (sizeActions.Contains(action))
            {
                var size = fileDetailsService.Size(filePath);
                Console.WriteLine($"File {filePath} has size of {size} bytes");
                return;
            }

            if (versionActions.Contains(action))
            {
                var version = fileDetailsService.Version(filePath);
                Console.WriteLine($"File {filePath} is at version {version}");
                return;
            }

            Console.WriteLine("Action not recognised, allowed actions include -v, --v, /v, --version for the file version, " +
                "and -s, --s, /s, --size for the file size");
        }

        private static IServiceProvider ConfigureDI()
        {
            var services = new ServiceCollection()
                .AddTransient<IFileDetailsService, FileDetailsService>();

            return services.BuildServiceProvider();
        }
    }
}
