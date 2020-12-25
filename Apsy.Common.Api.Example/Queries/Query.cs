using Apsy.Elemental.Example.Admin.Data;
using Apsy.Example.Services;
using GraphQL.Types;

namespace Apsy.Example.Queries
{
    partial class Query : ObjectGraphType
    {
        private readonly SingltonDataContextService<DataContext> singltonDataContextService;

        public Query(SingltonDataContextService<DataContext> singltonDataContextService)
        {
            Name = "Query";
            this.singltonDataContextService = singltonDataContextService;
            InitializePost();
        }
    }
}