using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IGT.SwaggerUI.AspNetCore.OData.Extensions;
using Microsoft.OpenApi.Models;
using IGT.SwaggerUI.AspNetCore.OData;
using Microsoft.Extensions.Logging;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(ops =>
            {
                ops.Conventions.Add(new ODataSwaggerAppConvention(loggerFactory.CreateLogger(typeof(ODataSwaggerAppConvention))));
            });

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "IGT.Swashbuckle.OData.SampleWebApi", Version = "v1" });
            //     c.SupportNonNullableReferenceTypes();
            // });

            services.AddSwaggerWithOData(loggerFactory.CreateLogger<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // app.UseSwagger();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // UseOpenApiDocs(app);
            app.UseSwaggerWithOData(options =>
            {
                options.SwaggerUIOptions.DocumentTitle = "Custom OData API Documentation";
            });
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
