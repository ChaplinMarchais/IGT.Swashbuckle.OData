using IGT.SwaggerUI.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace IGT.SwaggerUI.AspNetCore.OData.Edm
{
    public static class DefaultEdmGenerator
    {
        public static IEdmModel GetEdmModel(DefaultEdmOptions options)
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.Namespace = options.Namespace;
            modelBuilder.ContainerName = options.ContainerName;
            modelBuilder.EnableLowerCamelCase();

            var odataControllerTypes = options.Target.GetChildTypesAssignableTo<ODataController>();

            var actionDTOTypes = odataControllerTypes.ResolveActionDTOs<EnableQueryAttribute>();

            if(actionDTOTypes is not null)
                foreach (var dtoType in actionDTOTypes)
                {
                    modelBuilder.AddEntityType(dtoType);
                }

            return modelBuilder.GetEdmModel();
        }
    }
}