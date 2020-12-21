using GraphQL.Types;
using System;

namespace Apsy.Common.Api.Graph
{
    public class GraphApi<T> : ObjectGraphType<T>
    {
        public GraphApi()
        {
            Name = typeof(T).Name;
            GraphBuilder.AddFields<T>(this);
        }
    }
}
