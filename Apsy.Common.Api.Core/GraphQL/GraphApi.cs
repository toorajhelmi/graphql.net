using GraphQL.Types;
using System;

namespace Apsy.Common.Api.Core.Graph
{
    public class GraphApi<T> : ObjectGraphType<T>
    {
        public GraphApi()
        {
            if (typeof(T).IsGenericType)
            {
                var name = typeof(T).Name;
                int index = name.IndexOf('`');
                var nonGenericName = name.Substring(0, index);
                //Name = $"{nonGenericName}_{typeof(T).GetGenericArguments()[0].Name}";
                Name = nonGenericName;
            }
            else if (typeof(T).IsArray)
            {
                Name = $"{typeof(T).GetElementType().Name}Array";
            }
            else
            {
                Name = typeof(T).Name;
            }

            GraphBuilder.AddFields<T>(this);
        }
    }
}
