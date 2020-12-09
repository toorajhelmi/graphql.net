using Apsy.Elemental.Core.Graph;

namespace Apsy.Elemental.Example.Web.Models
{
    public enum DiscountType
    {
        Percentage,
        Dollar
    }

    public class Discount
    {
        public int DiscountId { get; set; }
        [Api(false)]
        public string Code { get; set; }
        [Api]
        public DiscountType DiscountType { get; set; }
        [Api]
        public double Value { get; set; }
    }
}
