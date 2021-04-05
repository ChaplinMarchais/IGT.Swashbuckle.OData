using System.Reflection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwaggerWithOData(this IServiceCollection services, ILogger logger)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddOpenApiDocument();
            services.AddOData();
            logger.LogDebug($"Added SwaggerUI support for handling OData APIs");

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, IServiceProvider services, ODataSwaggerContext options)
        {
            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapSwaggerWithODataRoute(options);
            });

            app.UseSwaggerUI(c =>
            {
                if(options.SwaggerUIOptions.DocumentTitle is null){
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IGT.Swashbuckle.OData.SampleWebApi v1");
                } else {
                    c.SwaggerEndpoint(options.SwaggerInfoUrl, options.SwaggerUIOptions.DocumentTitle);
                }
            });
            return app;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, Action<ODataSwaggerContext>? optionsSetup = null)
        {
            // Create a new DI scope for the OData Middleware to use
            using var services = app.ApplicationServices.CreateScope();

            // Check to see if there is any configuration data that has been configured and monitor it for changes
            var configMonitor = services.ServiceProvider.GetRequiredService<IOptionsMonitor<ODataSwaggerContext>>();

            ODataSwaggerContext latestConfig = new ODataSwaggerContext();

            configMonitor.OnChange<ODataSwaggerContext>(x => GetLatestConfig(x, latestConfig));

            if (optionsSetup != null)
                optionsSetup?.Invoke(latestConfig);

            return app.UseSwaggerWithOData(services.ServiceProvider, latestConfig);
        }

        private static T? GetLatestConfig<T>(T updatedConfig, T latestConfig) => latestConfig?.GetHashCode() != updatedConfig?.GetHashCode()
                            ? updatedConfig
                            : latestConfig;
    }
}