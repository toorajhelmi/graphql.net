namespace Apsy.Elemental.Example.Web.Api
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
