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
using Microsoft.Extensions.Options;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerContext
    {
        private IAssemblyProvider _assemblyProvider;

        public SwaggerUIOptions SwaggerUIOptions { get { return _swaggerUIOptions ??= new SwaggerUIOptions(); } internal set => _swaggerUIOptions = value; }

        public IOptions<ODataSwaggerOptions> Options { get; }

        private SwaggerUIOptions? _swaggerUIOptions;

        public ODataSwaggerContext(IOptions<ODataSwaggerOptions> options, IAssemblyProvider assemblyProvider)
        {
            Options = options;
            _assemblyProvider = assemblyProvider;
        }

        internal IEnumerable<IEdmModel> ResolveEdmModels()
        {
            var builderOptions = new DefaultEdmOptions(_assemblyProvider);
            var models = DefaultEdmGenerator.GetEdmModels(builderOptions);

            return models;
        }
    }
}