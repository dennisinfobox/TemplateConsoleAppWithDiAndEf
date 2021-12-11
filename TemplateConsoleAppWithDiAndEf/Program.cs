using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TemplateConsoleAppWithDiAndEf
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    // uncomment for using secrets
                    // builder.AddUserSecrets<Program>();
                    builder.AddJsonFile("custom.json");
                })
                .UseStartup<Startup>();
    }
}
