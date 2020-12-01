using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;

namespace Apsy.Elemental.Example.Web.Models
{
    public class ItemPortion
    {
        [Api(false)]
        public int ItemPortionId { get; set; }
        [Api(false)]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        [Api(false)]
        public int PortionId { get; set; }
        [Api]
        public Portion Portion { get; set; }
        [Api]
        public string Description { get; set; }
        [Api]
        public double Price { get; set; }
        [Api]
        public int Calories { get; set; }
        [Api]
        public List<ItemIngredient> Ingredients { get; set; }
    }
}
