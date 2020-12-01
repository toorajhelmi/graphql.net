using GraphQL.Types;
using System;

namespace Apsy.Elemental.Core.Graph
{
    public class GraphEnumApi<T> : EnumerationGraphType<T>
        where T : Enum
    {
        public GraphEnumApi()
        {
            Name = typeof(T).Name;
        }
    }
}
