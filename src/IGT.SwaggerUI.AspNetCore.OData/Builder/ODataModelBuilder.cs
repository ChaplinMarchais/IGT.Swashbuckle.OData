using System.Reflection;
using System;
using Microsoft.OData.Edm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OData.ModelBuilder;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataModelBuilder : ODataConventionModelBuilder
    {
        public ODataEdmModel Edm => _edm ??= new ODataEdmModel();
        private ODataEdmModel? _edm = null;

        public ODataModelBuilder(string baseContainerName, ODataEdmModel? edm) : base()
        {
            if(edm is not null) _edm = edm;
            this.ContainerName = baseContainerName;
        }

        public ODataModelBuilder(string baseContainerName = "Default Base Container", Action<ODataEdmModel>? edmConfiguration = null) : this(baseContainerName, edm: null)
        {
            _edm ??= new ODataEdmModel();

            if (edmConfiguration is not null)
                edmConfiguration(_edm);
        }
    }
}