using System;
using System.ComponentModel.DataAnnotations;

namespace Apsy.Elemental.Example.Web.Models
{
    public class OperationHours
    {
        public int OperationHoursId { get; set; }
        [DataType(DataType.Time)]
        public DateTime From { get; set; }
        [DataType(DataType.Time)]
        public DateTime To { get; set; }
        [Required]
        [Display(Name="Serving")]
        public string Name { get; set; }
    }
}
