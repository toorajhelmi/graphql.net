using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class MenuItem
    {
        [Api(false)]
        public int MenuItemId { get; set; }
        public int MenuSectionID { get; set; }
        public MenuSection Section { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        [Api]
        public string Description { get; set; }
        [Api]
        public List<ItemPortion> Portions { get; set; }
        [Api]
        public string PhotoUIrl { get; set; }
        //public bool RecommendedItem { get; set; }
        [Api]
        public bool OutOfStuck { get; set; }
    }
}
