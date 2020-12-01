using Apsy.Elemental.Core.Graph;
using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Web.Services;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using System;
using System.Reactive.Linq;

namespace Apsy.Elemental.Example.Web.Api
{
    public class Subscription : ObjectGraphType
    {
        private OrderService orderService;

        public Subscription(OrderService orderService)
        {
            this.orderService = orderService;

            Name = "Subscription";
            AddField(new EventStreamFieldType
            {
                Name = "orderEvent",
                Arguments = new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "orderId" }),
                Type = typeof(GraphApi<Order>),
                Resolver = new FuncFieldResolver<Order>(Resolve),
                Subscriber = new EventStreamResolver<Order>(Subscribe)
            });
        }

        private Order Resolve(IResolveFieldContext context)
        {
            return context.Source as Order;
        }

        private IObservable<Order> Subscribe(IResolveEventStreamContext context)
        {
            var orderId = context.GetArgument<int>("orderId");
            var observer = orderService.EventStream().Where(order => order.OrderId == orderId);
            return observer;
        }
    }
}
