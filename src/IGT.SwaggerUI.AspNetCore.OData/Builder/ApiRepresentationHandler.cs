using System.Collections.Generic;
using System;
using Microsoft.OData.Edm;


namespace IGT.Swashbuckle.OData.Builder
{
    public class ApiRepresentationHandler
    {
        public IList<IEdmModel> EdmModels { get; set; }
    }
}