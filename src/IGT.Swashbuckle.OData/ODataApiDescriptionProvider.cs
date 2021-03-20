using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace IGT.Swashbuckle.OData
{
    internal class ODataApiDescriptionProvider : IApiDescriptionProvider
    {
        private ODataSwaggerConfiguration oDataSwaggerConfig;

        public ODataApiDescriptionProvider(ODataSwaggerConfiguration oDataSwaggerConfig)
        {
            this.oDataSwaggerConfig = oDataSwaggerConfig;
        }

        public int Order => throw new System.NotImplementedException();

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}