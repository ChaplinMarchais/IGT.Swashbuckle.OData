using System.Reflection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataModelBuilder
    {
        private readonly IEdmModel _edm = null;
        public IEdmModel Edm => _edm;

        private IEdmEntityContainer _baseContainer;

        public ODataModelBuilder(Action<IEdmEntityContainer> baseContainerSetup = null)
        {
            _baseContainer = new EdmEntityContainer(GetBaseNamespace(), "default");
            baseContainerSetup(_baseContainer);

            if(_edm is null){
                _edm = new ODataEdmModel();
            }

            string GetBaseNamespace() => this.GetType().Namespace;
        }
    }
}