using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    class DefaultAssemblyProvider : IAssemblyProvider
    {
        private readonly Lazy<IEnumerable<Assembly>> _assemblies;

        public DefaultAssemblyProvider(IWebHostEnvironment env)
        {
            if (env is null)
                throw new ArgumentNullException(nameof(env));

            _assemblies = new Lazy<IEnumerable<Assembly>>(() => GetAssemblies(env));
        }

        public IEnumerable<Assembly> ResolveAssemblies => _assemblies.Value;

        private static IEnumerable<Assembly> GetAssemblies(IWebHostEnvironment environment)
        {
            var parts = DefaultAssemblyPartDiscoveryProvider.DiscoverAssemblyParts(environment.ApplicationName);

            return parts.OfType<AssemblyPart>()
                        .Select(p => p.Assembly)
                        .ToArray();
        }
    }
}