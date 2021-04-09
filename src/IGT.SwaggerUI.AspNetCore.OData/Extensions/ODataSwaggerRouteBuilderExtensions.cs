using Microsoft.AspNetCore.Routing;
using Microsoft.OData.Edm;
using System;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class ODataSwaggerRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapSwaggerWithODataRoute(this IEndpointRouteBuilder builder, ODataSwaggerContext context, string? prefix = null)
        {
            var x = builder.ServiceProvider;
            var edm = context.ResolveEdmModels();

            return builder;
        }

        public static IEndpointRouteBuilder MapSwaggerWithODataRoute(this IEndpointRouteBuilder builder, string prefix, IEdmModel model)
        {
            var edm = model;

            return builder;
        }
    }
}