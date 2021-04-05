using Microsoft.AspNetCore.Routing;
using Microsoft.OData.Edm;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class ODataSwaggerRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapSwaggerWithODataRoute(this IEndpointRouteBuilder builder, ODataSwaggerContext context, string? prefix = null)
        {
            var edm = context.ResolveEdm(context.IncludeDefaultProvider);

            return builder;
        }

        public static IEndpointRouteBuilder MapSwaggerWithODataRoute(this IEndpointRouteBuilder builder, string prefix, IEdmModel model)
        {
            var edm = model;

            return builder;
        }
    }
}