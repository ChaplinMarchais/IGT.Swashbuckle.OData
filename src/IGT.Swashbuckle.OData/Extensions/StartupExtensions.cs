using System;
using Microsoft.AspNetCore.Builder;

namespace IGT.Swashbuckle.OData.Extensions
{
    public static class StartupExtensions
    {
        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, ODataSwaggerConfiguration docConfig)
        {
            return app;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, Action<ODataSwaggerConfiguration> docConfigSetup = null)
        {
            var config = new ODataSwaggerConfiguration();

            if (docConfigSetup != null)
            {
                docConfigSetup?.Invoke(config);
            }

            return app;
        }
    }
}