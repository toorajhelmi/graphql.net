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
                Name = AppendGenericTypeName("", typeof(T));
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

        private string AppendGenericTypeName(string currentName, Type type)
        {
            if (type.IsGenericType)
            {
                var name = type.Name;
                int index = name.IndexOf('`');
                var nonGenericName = name.Substring(0, index);
                return currentName + $"{nonGenericName}_{AppendGenericTypeName(currentName, type.GetGenericArguments()[0])}";
            }
            else
            {
                return currentName + type.Name;
            }
        }
    }
}
