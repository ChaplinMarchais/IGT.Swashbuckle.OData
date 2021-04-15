using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Logging;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerAppConvention : IApplicationModelConvention
    {
        private readonly ILogger _logger;

        public ODataSwaggerAppConvention(ILogger logger)
        {
            _logger = logger;
        }

        public void Apply(ApplicationModel application)
        {
            var controllers = application.Controllers;

            foreach (var controller in controllers)
            {
                var controllerType = controller.GetType();
                var isOdataController = controllerType.IsAssignableTo(typeof(ODataController));

                if(isOdataController)
                {
                    controller.ApiExplorer.IsVisible = true;
                    _logger.LogDebug($"Generating OData OpenAPI Documentation for `{controller.ControllerName}`");
                }
            }
        }
    }
}