using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class MenuSection
    {
        [Api(false)]
        public int MenuSectionID { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        [Api]
        public List<MenuItem> Items { get; set; }
    }
}
