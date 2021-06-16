using IDIQTest.ConsoleApp.Model;
using IDIQTest.Domain.Services;
using IDIQTest.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDIQTest.ConsoleApp
{
    class Program
    {
        private static string _baseDirectory = Directory.GetCurrentDirectory();
        private static Configuration _configSettings;
        private static IWebScrapperService _webScrapperService;
        private static IFileSaverService _fileSaverService;

        public static void Main(string[] args)
        {
            SetupApplication();
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var urls = args.Length > 0
                ? args
                : _configSettings != null && _configSettings.WebSites.Length > 0
                    ? _configSettings.WebSites
                    : Enumerable.Empty<string>();

            if (urls.Any())
            {
                foreach(var url in urls)
                {
                    await ScrapUrl(url);
                }
            }
            else
            {
                Console.WriteLine("Please, provide urls to scrap either in command line or in configuration");
            }
        }

        private static async Task ScrapUrl(string url)
        {
            try
            {
                var scrapResult = await _webScrapperService.GetContentAsync(url);
                await _fileSaverService.SaveContentAsync(scrapResult);
                Console.WriteLine($"{url}. Scraping completed");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{url}. Scraping failed. {ex.Message}");
            }
        }

        private static void SetupApplication()
        {
            GetConfigurationSettings();

            if (_configSettings!=null && !string.IsNullOrEmpty(_configSettings.BaseFolder))
            {
                _baseDirectory = _configSettings.BaseFolder;
            }

            _webScrapperService = new WebScrapperService();
            _fileSaverService = new FileSaverService(_baseDirectory);
        }

        private static void GetConfigurationSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            _configSettings = config.GetSection("Configuration").Get<Configuration>();
        }
    }
}
 