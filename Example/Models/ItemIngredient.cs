using Apsy.Elemental.Core.Graph;

namespace Apsy.Elemental.Example.Web.Models
{
    public class ItemIngredient
    {
        [Api(false)]
        public int ItemIngredientId { get; set; }
        [Api(false)]
        public int ItemPortionId { get; set; }
        public ItemPortion ItemPortion { get; set; }
        [Api]
        public int IngredientId { get; set; }
        [Api]
        public Ingredient Ingredient { get; set; }
        [Api]
        public int CaloriesDelta { get; set; }
        [Api]
        public double PriceDelta { get; set; }
        [Api]
        public bool Included { get; set; }
    }
}
