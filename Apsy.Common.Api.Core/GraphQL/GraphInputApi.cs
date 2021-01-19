using GraphQL.Types;

namespace Apsy.Common.Api.Core.Graph
{
    public class GraphInputApi<T> : InputObjectGraphType<T>
    {
        public GraphInputApi()
        {
            if (typeof(T).IsGenericType)
            {
                var name = typeof(T).Name;
                int index = name.IndexOf('`');
                var nonGenericName = name.Substring(0, index);

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
            Name = Name + "Input";
            GraphBuilder.AddFields<T>(this);
        }
    }
}
