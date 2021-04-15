using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IGT.SwaggerUI.AspNetCore.OData.Extensions;
using Microsoft.OpenApi.Models;
using IGT.SwaggerUI.AspNetCore.OData;
using Microsoft.Extensions.Logging;
using Lamar;

namespace IGT.Swashbuckle.OData.SampleWebApi
{
    public class Startup
    {
        private readonly ILoggerFactory loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by Lamar. Should be used as a stand-in for ConfigureServices()
        public void ConfigureContainer(ServiceRegistry services)
        {
            // services.AddMvc(ops =>
            //     {
            //         ops.Conventions.Add(new ODataSwaggerAppConvention(loggerFactory.CreateLogger(typeof(ODataSwaggerAppConvention))));
            //     })
            //     .AddControllersAsServices();

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "IGT.Swashbuckle.OData.SampleWebApi", Version = "v1" });
            //     c.SupportNonNullableReferenceTypes();
            // });

            services.AddSwaggerWithOData(Configuration, loggerFactory.CreateLogger("Debug"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // app.UseSwagger();
            }

            app.UseAuthorization();
            
            app.UseSwaggerWithOData((container, options) =>
            {
                //Configure any overrides for both SwaggerUI and SwaggerGen here
            });

            app.UseHttpsRedirection();
            // app.UseRouting();

            // UseOpenApiDocs(app);
        }

        // private static void UseOpenApiDocs(IApplicationBuilder app)
        // {
        //     app.UseSwaggerUi3(settings =>
        //     {
        //         // Config the SwaggerUI settings here
        //         settings.DocumentTitle = "OData API Documentation";
        //     });
        // }
    }
}
