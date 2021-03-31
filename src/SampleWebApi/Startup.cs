using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IGT.SwaggerUI.AspNetCore.OData.Configuration;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IGT.Swashbuckle.OData.SampleWebApi", Version = "v1" });
                c.SupportNonNullableReferenceTypes();
            });
            services.AddOpenApiDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerWithOData(options =>
                {
                    options.DocumentTitle = "Custom OData API Documentation";
                });
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IGT.Swashbuckle.OData.SampleWebApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            UseOpenApiDocs(app);
        }

        private static void UseOpenApiDocs(IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                // Config the SwaggerUI settings here
                settings.DocumentTitle = "OData API Documentation";
            });
        }
    }
}
