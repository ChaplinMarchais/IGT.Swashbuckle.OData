using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace IGT.SwaggerUI.AspNetCore.OData.Configuration
{
    public static class StartupExtensions
    {
        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, ODataSwaggerConfiguration docConfig)
        {
            return app;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, Action<ODataSwaggerConfiguration> docConfigSetup = null)
        {
            var services = app.ApplicationServices;
            var configMonitor = services.GetRequiredService<IOptionsMonitor<ODataSwaggerConfiguration>>();

            ODataSwaggerConfiguration latestConfig = new ODataSwaggerConfiguration();

            configMonitor.OnChange<ODataSwaggerConfiguration>(x => GetLatestConfig(x));

            if (docConfigSetup != null)
                docConfigSetup?.Invoke(latestConfig);

            return app.UseSwaggerWithOData(latestConfig);

            ODataSwaggerConfiguration GetLatestConfig(ODataSwaggerConfiguration updatedConfig)
                => latestConfig.GetHashCode() != updatedConfig.GetHashCode()
                    ? updatedConfig
                    : latestConfig;
        }
    }
}