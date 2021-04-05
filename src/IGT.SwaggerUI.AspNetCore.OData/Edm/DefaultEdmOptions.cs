using System.Linq;
using System.Reflection;

namespace IGT.SwaggerUI.AspNetCore.OData.Edm
{
    public struct DefaultEdmOptions
    {
        public DefaultEdmOptions(Assembly target)
        {
            Target = target;
            Namespace = Target.DefinedTypes.FirstOrDefault()?.Namespace ?? "IGT.FC.Data.Edm";
            ContainerName = Target.FullName ?? Namespace;
        }

        public string Namespace { get; private set; }

        public string ContainerName {get; private set;}
        internal Assembly Target { get; }
    }
}