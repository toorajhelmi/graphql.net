using Apsy.Elemental.Core.Graph;
using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Web.Services;
using GraphQL;
using GraphQL.Types;

namespace Apsy.Elemental.Example.Web.Api
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(OrderService orderService, CustomerService customerService, SearchHistoryService searchHistoryService)
        {
            Name = "Mutation";
            AddOrderMutations(orderService);
            AddCustomerMutations(customerService);
            AddSearchHistoryMutations(searchHistoryService);
        }

        private void AddCustomerMutations(CustomerService customerService)
        {
            FieldAsync<GraphApi<Customer>>(
                "CreateCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GraphInputApi<Customer>>> { Name = "customer" }),
                resolve: async context =>
                {
                    var customer = context.GetArgument<Customer>("customer");
                    return await customerService.AddCustomer(customer);
                });

            FieldAsync<GraphApi<Customer>>(
                "UpdateCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "customerId" },
                    new QueryArgument<GraphInputApi<Customer>> { Name = "updatedCustomer" }),
                resolve: async context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    var updatedCustomer = context.GetArgument<Customer>("updatedCustomer");
                    return await customerService.UpdateCustomer(customerId, updatedCustomer);
                });
        }

        private void AddOrderMutations(OrderService orderService)
        {
            FieldAsync<GraphApi<Order>>(
                "CreateOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GraphInputApi<Order>>> { Name = "order" }),
                resolve: async context =>
                {
                    var order = context.GetArgument<Order>("order");
                    return await orderService.AddOrder(order);
                });

            FieldAsync<GraphApi<Order>>(
                "UpdateOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "orderId" },
                    new QueryArgument<GraphEnumApi<OrderStatus>> { Name = "status" }),
                resolve: async context =>
                {
                    var orderId = context.GetArgument<int>("orderId");
                    var status = context.GetArgument<OrderStatus>("status");
                    return await orderService.UpdateOrder(orderId, status);
                });
        }

        private void AddSearchHistoryMutations(SearchHistoryService searchHistoryService)
        {
            FieldAsync<GraphApi<SearchHistory>>(
                "CreateSearchHistory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GraphInputApi<SearchHistory>>> { Name = "searchHistory" }),
                resolve: async context =>
                {
                    var searchHistory = context.GetArgument<SearchHistory>("searchHistory");
                    return await searchHistoryService.AddSearchHistory(searchHistory);
                });

        }
    }
}

