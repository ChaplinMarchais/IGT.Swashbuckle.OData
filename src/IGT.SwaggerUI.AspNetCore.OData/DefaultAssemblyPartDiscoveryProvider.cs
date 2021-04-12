using System.Diagnostics;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyModel;

namespace IGT.SwaggerUI.AspNetCore.OData
{
    internal static class DefaultAssemblyPartDiscoveryProvider
    {
        internal static HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>(StringComparer.Ordinal)
        {
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Mvc.Abstractions",
            "Microsoft.AspNetCore.Mvc.ApiExplorer",
            "Microsoft.AspNetCore.Mvc.Core",
            "Microsoft.AspNetCore.Mvc.Cors",
            "Microsoft.AspNetCore.Mvc.DataAnnotations",
            "Microsoft.AspNetCore.Mvc.Formatters.Json",
            "Microsoft.AspNetCore.Mvc.Formatters.Xml",
            "Microsoft.AspNetCore.Mvc.Localization",
            "Microsoft.AspNetCore.Mvc.Razor",
            "Microsoft.AspNetCore.Mvc.Razor.Host",
            "Microsoft.AspNetCore.Mvc.TagHelpers",
            "Microsoft.AspNetCore.Mvc.ViewFeatures",
            "Microsoft.Extensions.Configuration"
        };

        internal static IEnumerable<ApplicationPart> DiscoverAssemblyParts(string entryAssemblyName)
        {
            var entryAssembly = Assembly.Load(new AssemblyName(entryAssemblyName));
            var context = DependencyContext.Load(Assembly.Load(new AssemblyName(entryAssemblyName)));

            return GetAssemblies(entryAssembly, context).Select(p => new AssemblyPart(p));
        }

        internal static IEnumerable<Assembly?> GetAssemblies(Assembly entryAssembly, DependencyContext context)
        {
            if(context is null)
                return new[] { entryAssembly };

            return GetLibraries(context)
                .SelectMany<RuntimeLibrary, string?>(lib => lib.RuntimeAssemblyGroups?.GetDefaultGroup()?.AssetPaths ?? Enumerable.Empty<string>())
                .Select(Load)
                .Where(asm => asm is not null);
        }

        private static IEnumerable<RuntimeLibrary> GetLibraries(DependencyContext context)
        {
            return context is null ?
                Enumerable.Empty<RuntimeLibrary>()
                : context.RuntimeLibraries.Where(IsCandidateLibrary);

            static bool IsCandidateLibrary(RuntimeLibrary library)
                => library.Dependencies.Any(dependency => ReferenceAssemblies.Contains(dependency.Name));
        }

        private static Assembly? Load(string? assetPath)
        {
            var name = Path.GetFileNameWithoutExtension(assetPath);
            if (name is not null)
            {
                if(name.EndsWith(".ni", StringComparison.OrdinalIgnoreCase))
                    name = name.Substring(0, name.Length - 3);

                return Assembly.Load(new AssemblyName(name));
            }

            return null;
        }
    }
}