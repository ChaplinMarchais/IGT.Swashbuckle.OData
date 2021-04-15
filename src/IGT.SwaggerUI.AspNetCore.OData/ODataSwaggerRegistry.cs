using Lamar;
using Microsoft.Extensions.Options;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public class ODataSwaggerRegistry : ServiceRegistry
    {
        public ODataSwaggerRegistry()
        {
            For<IAssemblyProvider>().Use<DefaultAssemblyProvider>();

            ForConcreteType<ODataSwaggerContext>().Configure
                .Ctor<IAssemblyProvider>()
                    .Is(s => s.GetInstance<IAssemblyProvider>())
                .Ctor<IOptions<ODataSwaggerOptions>>();
        }
    }
}