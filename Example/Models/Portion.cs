using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Portion
    {
        [Api(false)]
        public int PortionId { get; set; }
        [Api(false)]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        [Api]
        public bool IsDefault { get; set; }
        [Api]
        public List<ItemPortion> ItemPortions { get; set; }
    }
}
