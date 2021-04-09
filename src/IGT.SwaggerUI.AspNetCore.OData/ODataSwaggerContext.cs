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
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerContext
    {
        private const string DEFAULT_SWAGGER_INFO_ENDPOINT = "/swagger/v1/swagger.json";
        private IAssemblyProvider _assemblyProvider;

        public ODataSwaggerContext(IAssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }

        public string SwaggerInfoUrl { get; set; } = DEFAULT_SWAGGER_INFO_ENDPOINT;

        public SwaggerUIOptions SwaggerUIOptions { get { return _swaggerUIOptions ??= new SwaggerUIOptions(); } set => _swaggerUIOptions = value; }
        private SwaggerUIOptions? _swaggerUIOptions;

        internal IEnumerable<IEdmModel> ResolveEdmModels()
        {
            var builderOptions = new DefaultEdmOptions(_assemblyProvider);
            var models = DefaultEdmGenerator.GetEdmModels(builderOptions);

            return models;
        }
    }
}