using GraphQL.Types;
using System;

namespace Apsy.Example.Mutations
{
    public partial class Mutation : ObjectGraphType
    {
        private IServiceProvider serviceProvider;

        public Mutation(IServiceProvider serviceProvider)
        {
            Name = "Mutation";
            this.serviceProvider = serviceProvider;
            InitializePost();
        }
    }
}
