using System.Reflection;
using System;
using IGT.SwaggerUI.AspNetCore.OData.Edm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerContext
    {
        public bool IncludeDefaultProvider { get; set; } = true;
        public string SwaggerInfoUrl { get; set; } = "/swagger/v1/swagger.json";

        public SwaggerUIOptions SwaggerUIOptions { get { return _swaggerUIOptions ??= new SwaggerUIOptions(); } set => _swaggerUIOptions = value; }
        private SwaggerUIOptions? _swaggerUIOptions;

        internal IEdmModel ResolveEdm(bool includeDefaultProvider)
        {
            var builderOptions = new DefaultEdmOptions(Assembly.GetEntryAssembly()!);
            var model = DefaultEdmGenerator.GetEdmModel(builderOptions);

            return model;
        }
    }
}