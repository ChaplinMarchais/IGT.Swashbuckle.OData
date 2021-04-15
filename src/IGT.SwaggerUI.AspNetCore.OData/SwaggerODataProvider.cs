using System;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class SwaggerODataProvider : ISwaggerProvider
    {
        private readonly ODataSwaggerContext docConfig;

        public SwaggerODataProvider(ODataSwaggerContext docConfig)
        {
            this.docConfig = docConfig;
        }

        public OpenApiDocument GetSwagger(string documentName, string? host = null, string? basePath = null)
        {
            throw new NotImplementedException();
        }
    }
}
