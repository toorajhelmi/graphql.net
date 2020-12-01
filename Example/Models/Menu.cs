using Apsy.Elemental.Core.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Menu
    {
        [Api(false)]
        public int MenuId { get; set; }
        [Api(false)]
        [Required]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        [Api]
        [Required]
        [Display(Name = "Menu Name")]
        public string Name { get; set; }
        [Api]
        public List<MenuSection> Sections { get; set; }
        [Api]
        [DataType(DataType.Time)]
        public DateTime AvailableFrom { get; set; }
        [Api]
        [DataType(DataType.Time)]
        public DateTime AvailabelTo { get; set; }
    }
}
