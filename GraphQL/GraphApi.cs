using GraphQL.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Apsy.Elemental.Core.Graph
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
