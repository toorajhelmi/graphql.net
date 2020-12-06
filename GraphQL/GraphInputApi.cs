using GraphQL.Types;
using System;

namespace Apsy.Elemental.Core.Graph
{
    public class GraphInputApi<T> : InputObjectGraphType<T>
    {
        public GraphInputApi()
        {
            Name = $"GraphInput{Guid.NewGuid().ToString().Replace("-", "")}";
            GraphBuilder.AddFields<T>(this);
        }
    }
}
