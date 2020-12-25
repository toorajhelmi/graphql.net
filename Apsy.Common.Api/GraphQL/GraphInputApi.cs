using GraphQL.Types;
using System;

namespace Apsy.Common.Api.Graph
{
    public class GraphInputApi<T> : InputObjectGraphType<T>
    {
        public GraphInputApi()
        {
            Name = typeof(T).Name + "Input";
            GraphBuilder.AddFields<T>(this);
        }
    }
}
