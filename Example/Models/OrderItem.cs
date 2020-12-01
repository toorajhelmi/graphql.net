using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;

namespace Apsy.Elemental.Example.Web.Models
{
    public class OrderItem
    {
        [Api(false)]
        public int OrderItemId { get; set; }
        [Api(false)]
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        [Api]
        public int ItemPortionId { get; set; }
        public ItemPortion ItemPortion { get; set; }
        [Api]
        public List<OrderItemChange> OrderItemChanges { get; set; }
        [Api]
        public string Instructions { get; set; }
    }
}

