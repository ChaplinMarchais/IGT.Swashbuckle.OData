using Microsoft.OpenApi.Models;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerOptions
    {
        public const string DEFAULT_SWAGGER_INFO_ENDPOINT = "/swagger/v1/swagger.json";

        public bool UseReDoc {get; set;} = false;
        public bool UseSwaggerUI {get; set;} = true;

        public OpenApiInfo? OpenApiInfo {get; set;}

    }
}