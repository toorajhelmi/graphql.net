using System.ComponentModel.DataAnnotations;
using Apsy.Elemental.Core.Graph;

namespace Apsy.Elemental.Example.Web.Models
{
    public class CustomerAddress
    {
        public int CustomerAddressId { get; set; }

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
        public bool IsPrimary { get; set; }

        [Api(false)]
        public int CustomerId { get; set; }
        [Api]
        public Customer Customer { get; set; }
    }
}