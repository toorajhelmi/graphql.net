using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        [Required]
        [Display(Name = "Restaurant Name")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }
        [Required]
        [Api]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Api]
        [Required]
        public string City { get; set; }
        [Api]
        [Required]
        public string State { get; set; }
        [Api]
        [Required]
        [RegularExpression(@"^\d{5}-\d{4}|\d{5}|[A-Z]\d[A-Z] \d[A-Z]\d$")]
        public string Zip { get; set; }
        [Api]
        [Required]
        public string Country { get; set; }
        [Api]
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Api]
        [EmailAddress]
        public string Email { get; set; }
        public List<OperationHours> OperationHours { get; set; }
        public List<Order> Orders { get; set; }
    }
}
