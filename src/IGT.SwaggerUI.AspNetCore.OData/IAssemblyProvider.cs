using System.Collections.Generic;
using System.Reflection;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> ResolveAssemblies { get; }
    }
}