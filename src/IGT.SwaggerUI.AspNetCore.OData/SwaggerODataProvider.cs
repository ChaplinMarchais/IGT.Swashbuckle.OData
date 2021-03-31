using System;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using IGT.SwaggerUI.AspNetCore.OData.Configuration;
namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class SwaggerODataProvider : ISwaggerProvider
    {
        private readonly ODataSwaggerConfiguration docConfig;

        public SwaggerODataProvider(ODataSwaggerConfiguration docConfig)
        {
            this.docConfig = docConfig;
        }

        public OpenApiDocument GetSwagger(string documentName, string? host = null, string? basePath = null)
        {
            throw new NotImplementedException();
        }
    }
}
