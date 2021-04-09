using StructureMap;
using StructureMap.Pipeline;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerRegistry : Registry
    {
        public ODataSwaggerRegistry()
        {
            For<IAssemblyProvider>().LifecycleIs(Lifecycles.Singleton)
                                    .Use<DefaultAssemblyProvider>();
        }
    }
}