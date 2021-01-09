using GraphQL.Types;
using System;

namespace Apsy.Common.Api.Core.Graph
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
