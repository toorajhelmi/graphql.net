using System;
using System.ComponentModel.DataAnnotations;
using Apsy.Elemental.Core.Graph;

namespace Apsy.Elemental.Example.Web.Models
{
    public class SearchHistory
    {
        public int SearchHistoryId { get; set; }

        [Api]
        [Display(Name = "Term")]
        public string Term { get; set; }

        [Api(false)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Api]
        public int CustomerId { get; set; }
        [Api]
        public Customer Customer { get; set; }
    }
}