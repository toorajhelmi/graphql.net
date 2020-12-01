using System;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int OrderId { get; set; }
        public int Order { get; set; }
        public DateTime ReviewedOn { get; set; }
        public int FoodRating { get; set; }
        public int DeliveryRating { get; set; }
        public string Notes { get; set; }
    }
}
