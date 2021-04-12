using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace IGT.Swashbuckle.OData.SampleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args)
        {
            var host = new WebHostBuilder()
                .UseLamar()
                .ConfigureAppConfiguration(cbuilder => {
                    cbuilder.AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile("appsettings.development.json", true)
                    .AddUserSecrets<Program>(true);
                    })
                .UseKestrel()
                .ConfigureLogging(lb => lb.AddDebug().AddConsole())
                .UseStartup<Startup>();

            return host.Build();
        }
    }
}
