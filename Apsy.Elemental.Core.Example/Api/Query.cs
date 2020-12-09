using Apsy.Elemental.Core.Graph;
using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Web.Services;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Apsy.Elemental.Example.Web.Api
{
    public class Query : ObjectGraphType
    {
        public Query(SingltonDataContextService singltonDataContextService)
        {
            Name = "query";
            Restaurants(singltonDataContextService);
            Customer(singltonDataContextService);
            Menu(singltonDataContextService);
            MenuItems(singltonDataContextService);
            CustomerAddresses(singltonDataContextService);
            CustomerOrderHistories(singltonDataContextService);
            SearchHistories(singltonDataContextService);
        }

        private void Restaurants(SingltonDataContextService singltonDataContextService)
        {
            Field<ListGraphType<GraphApi<Restaurant>>>(
                "restaurants", resolve: context =>
                {
                    List<Restaurant> restaurants = null; ;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        restaurants = dataContext.Restaurant
                            .Include(r => r.Branches)
                            .ToList();
                    });

                    return restaurants;
                });
        }

        private void Customer(SingltonDataContextService singltonDataContextService)
        {
            Field<GraphApi<Customer>>(
                name: "customer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" },
                    new QueryArgument<GraphEnumApi<OrderStatus>> { Name = "orderStatus" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var orderStatus = context.GetArgument<OrderStatus>("orderStatus");
                    Customer customer = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        customer = dataContext.Customer
                            .IncludeFilter(c => c.Orders.Where(o => o.OrderStatus == orderStatus))
                            .Include(c => c.Orders)
                                .ThenInclude(o => o.OrderItems)
                                    .ThenInclude(i => i.OrderItemChanges)
                                        .ThenInclude(c => c.ItemIngredient)
                            .Include(c => c.Orders)
                                .ThenInclude(o => o.OrderItems)
                                    .ThenInclude(i => i.ItemPortion)
                            .First(c => c.CustomerId == id);
                    });

                    return customer;
                });
        }

        private void Menu(SingltonDataContextService singltonDataContextService)
        {
            Field<GraphApi<Menu>>(
                name: "menu",
                 arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "restaurantId" }
                ),
                resolve: context =>
                {
                    var restaurantId = context.GetArgument<int>("restaurantId");
                    Menu menu = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        menu = dataContext.Menu
                            .Include(m => m.Sections).ThenInclude(s => s.Items).ThenInclude(i => i.Portions)
                                .ThenInclude(p => p.Ingredients).ThenInclude(i => i.Ingredient).ThenInclude(i => i.IngredientCategory)
                            .Include(m => m.Sections).ThenInclude(s => s.Items).ThenInclude(i => i.Portions)
                                .ThenInclude(p => p.Portion)
                            .First(m => m.RestaurantId == restaurantId);
                    });

                    return menu;
                });
        }

        private void MenuItems(SingltonDataContextService singltonDataContextService)
        {
            Field<ListGraphType<GraphApi<MenuItem>>>(
                name: "menuItems",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "restaurantId" }
                ),
                resolve: context =>
                {
                    var restaurantId = context.GetArgument<int>("restaurantId");
                    List<MenuItem> items = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        items = dataContext.MenuItem
                            .Where(i => i.Section.Menu.RestaurantId == restaurantId)
                            .ToList();
                    });

                    return items;
                });
        }

        private void CustomerOrderHistories(SingltonDataContextService singltonDataContextService)
        {
            Field<ListGraphType<GraphApi<Order>>>(
                name: "customerOrderHistories",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "customerId" }
                ),
                resolve: context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    List<Order> items = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        items = dataContext.Order
                            .Where(i => i.CustomerId == customerId)
                            .OrderByDescending(a => a.OrderTime)
                            .ToList();
                    });

                    return items;
                });
        }

        private void CustomerAddresses(SingltonDataContextService singltonDataContextService)
        {
            Field<ListGraphType<GraphApi<CustomerAddress>>>(
                name: "customerAddresses",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "customerId" }
                ),
                resolve: context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    List<CustomerAddress> items = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        items = dataContext.CustomerAddress
                            .Where(i => i.CustomerId == customerId)
                            .ToList();
                    });

                    return items;
                });
        }


        private void SearchHistories(SingltonDataContextService singltonDataContextService)
        {
            Field<ListGraphType<GraphApi<SearchHistory>>>(
                name: "customerSearchHistories",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "customerId" }
                ),
                resolve: context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    List<SearchHistory> items = null;

                    singltonDataContextService.Execute(dataContext =>
                    {
                        items = dataContext.SearchHistory
                            .Where(i => i.CustomerId == customerId)
                            .OrderByDescending(a => a.CreatedOn)
                            .ToList();
                    });

                    return items;
                });
        }
    }
}
