using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Restaurant
    {
        [Api]
        public int RestaurantId { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        [Api]
        public List<Branch> Branches { get; set; }
        [Api]
        public List<Menu> Menus { get; set; }
        [Api]
        public List<Portion> Portions { get; set; }
        [Api]
        public List<IngredientCategory> IngredientCategories { get; set; }
    }
}
