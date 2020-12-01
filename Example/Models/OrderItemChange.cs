using Apsy.Elemental.Core.Graph;

namespace Apsy.Elemental.Example.Web.Models
{
    public enum IngredientChangeType
    {
        Add,
        Remove
    }

    public class OrderItemChange
    {
        [Api(false)]
        public int OrderItemChangeId { get; set; }
        [Api(false)]
        public int? OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        [Api]
        public int ItemIngredientId { get; set; }
        public ItemIngredient ItemIngredient { get; set; }
        [Api]
        public IngredientChangeType IngredientChangeType { get; set; }
    }
}
