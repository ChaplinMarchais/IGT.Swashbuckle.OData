using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerUIMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ILoggerFactory loggerFactory;
        private readonly ODataSwaggerContext options;

        public ODataSwaggerUIMiddleware(RequestDelegate next,
            IWebHostEnvironment hostEnvironment,
            ILoggerFactory loggerFactory,
            ODataSwaggerContext options)
        {
            this.next = next;
            this.hostEnvironment = hostEnvironment;
            this.loggerFactory = loggerFactory;
            this.options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await Task.CompletedTask;
        }
    }
}