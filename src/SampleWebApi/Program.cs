using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                .UseKestrel()
                .ConfigureLogging(lb => lb.AddDebug())
                .UseStartup<Startup>();

            return host.Build();
        }
    }
}
