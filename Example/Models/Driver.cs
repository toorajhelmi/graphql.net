using Apsy.Elemental.Example.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        [Required]
        public string Name { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int OrderId { get; set; }
        public List<Order> OrderHistory { get; set; }
        public double Rating { get; set; }
    }
}
