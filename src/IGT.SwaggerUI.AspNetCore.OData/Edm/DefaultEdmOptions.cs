using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IGT.SwaggerUI.AspNetCore.OData.Edm
{
    public struct DefaultEdmOptions
    {
        private readonly IEnumerable<Tuple<string, Assembly>> targets;

        public DefaultEdmOptions(IAssemblyProvider assemblyProvider)
        {
            var assemblies = assemblyProvider.ResolveAssemblies;
            targets = assemblies.Select<Assembly, Tuple<string, Assembly>>(a => new(a.GetName().FullName, a));
        }

        public IEnumerable<Tuple<string, Assembly>> Targets => targets;
    }
}