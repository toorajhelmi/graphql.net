using Apsy.Common.Api.Core.Graph;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using System;
using System.Reactive.Linq;

namespace Apsy.Common.Api.Core.GraphQL
{
    public class SubscriptionBase : ObjectGraphType
    {
        protected EventStreamFieldType CreateObjectSubscription<T>(ObservableService<T> observableService,
            string eventName, string idProperyName, Func<Observable<T>, int> idSelector)
        {
            return new EventStreamFieldType
            {
                Name = eventName,
                Arguments = new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = idProperyName }),
                Type = typeof(GraphApi<Observable<T>>),
                Resolver = new FuncFieldResolver<Observable<T>>(c => c.Source as Observable<T>),
                Subscriber = new EventStreamResolver<Observable<T>>(c =>
                {
                    var id = c.GetArgument<int>(idProperyName);
                    var observer = observableService.EventStream().Where(o => idSelector(o) == id);
                    return observer;
                })
            };
        }
    }
}
