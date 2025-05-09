using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FindChipsScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var scraper = host.Services.GetRequiredService<FindChipsService>();
            await scraper.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHttpClient(); // Registers IHttpClientFactory
                    services.AddSingleton<FindChipsService>();
                });
    }
}
