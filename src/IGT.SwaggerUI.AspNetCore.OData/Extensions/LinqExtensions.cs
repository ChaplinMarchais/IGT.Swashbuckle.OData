using System;
using System.Collections.Generic;

namespace IGT.SwaggerUI.AspNetCore.OData.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> DoFor<T,U>(this IEnumerable<T> source, Predicate<T> predicate, Func<IEnumerable<T>, IEnumerable<U>> operation, out IEnumerable<U>? result)
        {
            result = operation(source);

            return source;
        }
    }
}