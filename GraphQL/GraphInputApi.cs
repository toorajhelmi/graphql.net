using GraphQL.Types;

namespace Apsy.Elemental.Core.Graph
{
    public class GraphInputApi<T> : InputObjectGraphType<T>
    {   
        public GraphInputApi()
        {
            Name = $"{typeof(T).Name}Input";

            GraphBuilder.AddFields<T>(this);
        }
    }
}
