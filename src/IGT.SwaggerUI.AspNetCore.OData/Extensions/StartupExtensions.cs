using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc;
using Lamar;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.OData.Formatter;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class StartupExtensions
    {
        public static ServiceRegistry AddSwaggerWithOData(this ServiceRegistry services, IConfiguration configuration, ILogger logger)
        {
            services.AddMvc(options => {
                foreach (var formatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var formatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.Configure<ODataSwaggerOptions>(configuration.GetSection("ODataSwaggerOptions"));
            services.IncludeRegistry<ODataSwaggerRegistry>();

            services.AddSwaggerGen(ops => {
                ops.SupportNonNullableReferenceTypes();
                ops.UseAllOfToExtendReferenceSchemas();
                ops.SwaggerDoc("v1", new OpenApiInfo {Title = "TEST TITLE", Version = "v1"});
            });

            logger.LogDebug("Configuring OData Core Services...");
            services.AddOData((options, svcs) => {
                var odataContext = svcs.GetRequiredService<ODataSwaggerContext>();
                foreach(var edm in odataContext.ResolveEdmModels())
                {
                    string name = edm.EntityContainer.Name;

                    logger.LogDebug($"\tRegistered EDM with Prefix: [{name}]");
                    options.AddModel(name, edm);
                }
            });
            logger.LogDebug($"--> DONE <--");

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithOData(this IApplicationBuilder app, ODataSwaggerContext context)
        {
            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if(context.SwaggerUIOptions.DocumentTitle is null){
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IGT.Swashbuckle.OData.SampleWebApi v1");
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