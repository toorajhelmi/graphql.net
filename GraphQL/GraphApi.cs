using GraphQL.Types;
using System;

namespace Apsy.Elemental.Core.Graph
{
    public class GraphApi<T> : ObjectGraphType<T>
    {
        public GraphApi()
        {
            Name = $"Graph{Guid.NewGuid().ToString().Replace("-", "")}";
            GraphBuilder.AddFields<T>(this);
        }
    }
}
