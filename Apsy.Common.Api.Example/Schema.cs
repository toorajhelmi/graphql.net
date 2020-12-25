using Apsy.Example.Mutations;
using Apsy.Example.Queries;
using Apsy.Example.Subscriptions;

namespace Apsy.Example.Api
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(Query query, Mutation mutation, Subscription subscription)
        {
            Query = query;
            Mutation = mutation;
            Subscription = subscription;
        }
    }
}
