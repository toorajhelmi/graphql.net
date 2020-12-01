using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class SavedPaymentCard
    {
        public int SavedPaymentCardId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public string Name { get; set; }
        public string Token { get; set; }
        public bool IsDefault { get; set; }
    }
}
