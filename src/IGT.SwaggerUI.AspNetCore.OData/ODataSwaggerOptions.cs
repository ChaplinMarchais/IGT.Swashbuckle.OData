using Microsoft.OpenApi.Models;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerOptions
    {
        public const string DEFAULT_SWAGGER_INFO_ENDPOINT = "/swagger/v1/swagger.json";

        public bool UseReDoc {get; set;} = false;
        public bool UseSwaggerUI {get; set;} = true;

        /// <summary>
        /// Set this property to indicate if a <see cref="DbContext"/> should be generated
        /// for each of the registered EDM Entity types.
        /// </summary>
        /// <value>Defaults to => False</value>
        public bool UseInMemoryDbForEntities {get; set;} = false;

        public OpenApiInfo? OpenApiInfo {get; set;}

    }
}