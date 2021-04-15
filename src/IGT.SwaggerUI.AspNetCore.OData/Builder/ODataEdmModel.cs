using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OData.Edm;


namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataEdmModel : EdmModel
    {
        public IApiDescriptionGroupCollectionProvider? ApiDescriptionProvider { get; set; }
    }
}