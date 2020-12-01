using Apsy.Elemental.Example.Web.Models;
using System;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Apsy.Elemental.Example.Web.Services
{
    public class OrderService
    {
        private readonly SingltonDataContextService singltonDataContextService;
        private readonly ISubject<Order> eventStream = new ReplaySubject<Order>(1);

        public OrderService(SingltonDataContextService singltonDataContextService)
        {
            this.singltonDataContextService = singltonDataContextService;
        }

        public IObservable<Order> EventStream()
        {
            return eventStream.AsObservable();
        }

        public async Task<Order> AddOrder(Order order)
        {
            return await singltonDataContextService.Execute<Order>(async dataContext =>
            {
                foreach (var item in order.OrderItems)
                {
                    item.ItemPortion = dataContext.ItemPortion.Find(item.ItemPortionId);
                    
                    foreach (var change in item.OrderItemChanges)
                    {
                        change.ItemIngredient = dataContext.ItemIngredient.Find(change.ItemIngredientId);
                    }
                }

            
                order.SubTotal = order.OrderItems.Sum(item =>      
                    item.ItemPortion.Price +
                    item.OrderItemChanges
                        .Where(change => change.IngredientChangeType == IngredientChangeType.Add)
                        .Sum(change => change.ItemIngredient.PriceDelta) -
                    item.OrderItemChanges
                        .Where(change => change.IngredientChangeType == IngredientChangeType.Remove)
                        .Sum(change => change.ItemIngredient.PriceDelta));

                if (!string.IsNullOrEmpty(order.DiscountCode))
                {
                    var discount = dataContext.Discount.FirstOrDefault(d => d.Code == order.DiscountCode);
                    if (discount != null)
                    {
                        order.Discounts = discount.DiscountType == DiscountType.Dollar ? discount.Value :
                           discount.Value * order.SubTotal / 100;
                    }
                    else
                    {
                        ///TODO: Exception
                    }
                }

                var taxRate = dataContext.Configuration.FirstOrDefault(c => c.Key == Constants.TaxeRate);
                if (taxRate != null && !string.IsNullOrEmpty(taxRate.Value))
                {
                    order.Taxes = order.SubTotal * double.Parse(taxRate.Value) / 100;
                }
                else
                {
                    ///TODO: Exception
                }

                if (order.OrderType == OrderType.Delivery)
                {
                    var serviceCharge = dataContext.Configuration.FirstOrDefault(c => c.Key == Constants.ServiceCharge);
                    if (serviceCharge != null && !string.IsNullOrEmpty(serviceCharge.Value))
                    {
                        order.ServiceCharge = double.Parse(serviceCharge.Value);
                    }
                    else
                    {
                        ///TODO: Exception
                    }

                    var deliveryCharge = dataContext.Configuration.FirstOrDefault(c => c.Key == Constants.DeliveryCharge);
                    if (deliveryCharge != null && !string.IsNullOrEmpty(deliveryCharge.Value))
                    {
                        order.DeliveryCharge = double.Parse(deliveryCharge.Value);
                    }
                    else
                    {
                        ///TODO: Exception
                    }
                }

                order.Total = order.SubTotal - order.Discounts + order.Taxes + order.ServiceCharge + order.DeliveryCharge;

                order.OrderTime = DateTime.UtcNow;
                order.OrderStatus = OrderStatus.Ordered;

                var orderEntry = dataContext.Order.Add(order);
                await dataContext.SaveChangesAsync();

                AddEvent(order);
                return orderEntry.Entity;
            });
        }

        public async Task<Order> UpdateOrder(int orderId, OrderStatus status)
        {
            return await singltonDataContextService.Execute<Order>(async dataContext =>
            {
                var order = dataContext.Order.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.OrderStatus = status;
                    await dataContext.SaveChangesAsync();
                    AddEvent(order);
                }
                else
                {
                    ///TODO: Exception
                }

                return order;
            });
        }

        private void AddError(Exception exception)
        {
            eventStream.OnError(exception);
        }

        private void AddEvent(Order order)
        {
            eventStream.OnNext(order);
        }

    }
}
