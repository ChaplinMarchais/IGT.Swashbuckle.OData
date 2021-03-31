using System;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Struct | AttributeTargets.Enum)]
    public class GenerateSwaggerDocsAttribute : Attribute
    {
        public GenerateSwaggerDocsAttribute()
        {

        }
    }
}