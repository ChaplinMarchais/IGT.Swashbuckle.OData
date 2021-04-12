using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Lamar;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class StartupExtensions
    {
        public static ServiceRegistry AddSwaggerWithOData(this ServiceRegistry services, IConfiguration configuration, ILogger logger)
        {
            services.Configure<ODataSwaggerOptions>(configuration.GetSection("ODataSwaggerOptions"));
            services.IncludeRegistry<ODataSwaggerRegistry>();

            services.AddSwaggerGen(ops => {
                ops.SupportNonNullableReferenceTypes();
                ops.SwaggerDoc("v1", new OpenApiInfo {Title = "TEST TITLE", Version = "v1"});
            });

            logger.LogDebug("Configuring OData Core Services...");
            services.AddControllers();
            services.AddOData((options, svcs) => {
                var odataContext = svcs.GetRequiredService<ODataSwaggerContext>();
                var edmModels = odataContext.ResolveEdmModels().ToArray();

                for(var i = 0; i < edmModels.Count(); i++)
                {
                    string name = edmModels[i].EntityContainer.Name;

                    logger.LogDebug($"\tRegistered EDM with Prefix: [{name}]");
                    
                    if(i == 0)
                        options.AddModel("odata", edmModels[i]).Filter().Select().Expand();
                    else
                        options.AddModel(name, edmModels[i]).Filter().Select().Expand();
                }
            });
            logger.LogDebug($"--> DONE <--");

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, ODataSwaggerContext context)
        {
            app.UseRouting();
            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapControllers();
            });

            app.UseSwagger(options => {
                options.RouteTemplate = "";
            });
            app.UseSwaggerUI(c =>
            {
                if(context.SwaggerUIOptions.DocumentTitle is null){
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                } else {
                    c.SwaggerEndpoint(ODataSwaggerOptions.DEFAULT_SWAGGER_INFO_ENDPOINT, context.SwaggerUIOptions.DocumentTitle);
                }
            });

            return app;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, Action<IServiceProvider, ODataSwaggerContext>? optionsSetup = null)
        {
            // Create a new DI scope for the OData Middleware to use
            var services = app.ApplicationServices;

            // Check to see if there is any configuration data that has been configured and monitor it for changes
            var context = services.GetRequiredService<ODataSwaggerContext>();

            if (optionsSetup != null)
                optionsSetup?.Invoke(services, context);

            return app.UseSwaggerWithOData(context);
        }
    }
}