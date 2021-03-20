using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace IGT.Swashbuckle.OData
{
    public class ODataSwaggerConfiguration
    {
        public SwaggerUIOptions swaggerUIOptions{ get; set; }
        public IApiDescriptionProvider GetApiDescriptionProvider
            => new ODataApiDescriptionProvider(this);

        public bool IncludeDefaultProvider { get; internal set; }
    }
}