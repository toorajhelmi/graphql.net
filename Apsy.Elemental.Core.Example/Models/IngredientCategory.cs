using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class IngredientCategory
    {
        [Api(false)]
        public int IngredientCategoryId { get; set; }
        [Api(false)]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
