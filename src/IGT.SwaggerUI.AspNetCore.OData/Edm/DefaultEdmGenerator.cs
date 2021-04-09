using System;
using System.Collections.Generic;
using System.Reflection;
using IGT.SwaggerUI.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace IGT.SwaggerUI.AspNetCore.OData.Edm
{
    public static class DefaultEdmGenerator
    {
        public static IEnumerable<IEdmModel> GetEdmModels(DefaultEdmOptions options)
        {
            foreach(var target in options.Targets)
            {
                (string name, Assembly asm) = target;

                var modelBuilder = new ODataConventionModelBuilder();
                modelBuilder.Namespace = name;
                modelBuilder.ContainerName = name;
                modelBuilder.EnableLowerCamelCase();

                var odataControllerTypes = asm.GetChildTypesAssignableTo<ODataController>();

                var actionDTOTypes = odataControllerTypes.ResolveActionDTOs<EnableQueryAttribute>();

                if(actionDTOTypes is not null)
                    foreach (var dtoType in actionDTOTypes)
                    {
                        modelBuilder.AddEntityType(dtoType);
                    }

                yield return modelBuilder.GetEdmModel();
            }
        }
    }
}