using Apsy.Elemental.Core.ApiDoc;
using Apsy.Elemental.Core.Graph;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Customer
    {
        [SwaggerIgnore]
        [Api(false)]
        public int CustomerId { get; set; }
        [Api(false)]
        public string UserId { get; set; }
        [Api]
        [Required]
        public string Name { get; set; }

        [Api]
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Api]
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        [Api]
        public string Password { get; set; }
        [Api]
        public string DeliveryInstructions { get; set; }
        [SwaggerIgnore]
        [Api]
        public List<SavedPaymentCard> Cards { get; set; }
        [SwaggerIgnore]
        [Api]
        public List<Order> Orders { get; set; }

        [Api]
        public List<CustomerAddress> CustomerAddresses { get; set; }

        [Api]
        public List<SearchHistory> SearchHistories { get; set; }


        public void CopyFrom(Customer other)
        {
            Name = other.Name;
            Phone = other.Phone;
            Email = other.Email;
            DeliveryInstructions = other.DeliveryInstructions;
            CustomerAddresses = other.CustomerAddresses;
            SearchHistories = other.SearchHistories;
        }
    }
}
