using Apsy.Elemental.Core.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public enum OrderStatus : int
    {
        Ordered,
        Pending,
        Prepared,
        Ready,
        PickedUp,
        Onroute,
        Delivered,
        Cancelled
    }

    public enum OrderType : int
    {
        Pickup,
        Delivery
    }

    public enum CancellationReason : int
    {
        CustomerRequest,
        OutofStock,
        DeliveryIssue,
        Other
    }

    public enum PaymentMethod
    {
        ApplePay,
        GooglePay,
        Paypal,
        Card
    }

    public class Order
    {
        [Api(false)]
        public int OrderId { get; set; }
        [Api]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
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
        [Api]
        public string DeliveryInstructions { get; set; }
        [Api]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        [Api]
        public PaymentMethod PaymentMethod { get; set; }
        [Api]
        public string DiscountCode { get; set; }

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        [Api]
        public List<OrderItem> OrderItems { get; set; }
        [Api]
        public OrderStatus OrderStatus { get; set; }
        [Api]
        public OrderType OrderType { get; set; }

        public double? SubTotal { get; set; }
        public double? Taxes { get; set; }
        public double? ServiceCharge { get; set; }
        public double? DeliveryCharge { get; set; }
        public double? Tip { get; set; }
        public double? Discounts { get; set; }
        public double? Total { get; set; }

        public DateTime OrderTime { get; set; }
        public DateTime ExpectedDeliveryTime { get; set; }
        public DateTime? EstimatedPickupTime { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? CancelledTime { get; set; }

        public CancellationReason CancellationReason { get; set; }
        
        public double? LastLattitude { get; set; }
        public double? LastLongitude { get; set; }

        public int? ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
