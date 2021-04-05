using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetChildTypesAssignableTo<T>(this Assembly target) where T : class
            => target.DefinedTypes.Where(t => t.IsAssignableTo(typeof(T)));

        public static IEnumerable<Type>? ResolveActionDTOs<TAttribute>(this IEnumerable<Type> controllers) where TAttribute : Attribute, new()
        {
            var attributeType = typeof(TAttribute);

            MemberFilter MemberIsODataActionCriteria = (memberInfo, _) => memberInfo.GetCustomAttributes<TAttribute>().Any();
            var actionMembers = controllers.SelectMany(c => c.FindMembers(MemberTypes.Method, BindingFlags.Public, MemberIsODataActionCriteria, null));

            return actionMembers.GetReturnDTOTypes();
        }

        public static IEnumerable<Type>? GetReturnDTOTypes(this IEnumerable<MemberInfo> actionMembersInfo)
        {
            actionMembersInfo
                .DoFor(m => ((MethodInfo)m).ReturnType.IsGenericType,
                            x => x.Select(m => ((MethodInfo)m).ReturnType.GenericTypeArguments[1]),
                            out var resultTypes1)
                .DoFor(m => !((MethodInfo)m).ReturnType.IsGenericType && ((MethodInfo)m).ReturnType != typeof(ActionResult),
                            x => x.Select(m => ((MethodInfo)m).ReturnType),
                            out var resultTypes2);

            var results = resultTypes1?.Concat(resultTypes2!);

            return results;
        }
    }


}