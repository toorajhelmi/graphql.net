using Apsy.Elemental.Core.Graph;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Ingredient
    {
        [Api(false)]
        public int IngredientId { get; set; }
        public int IngredientCategoryId { get; set; }
        [Api]
        public IngredientCategory IngredientCategory { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
    }
}
