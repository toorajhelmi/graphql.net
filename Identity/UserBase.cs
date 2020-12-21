using Apsy.Common.Api.Graph;

namespace Apsy.Common.Api.Identity
{
    public class UserBase
    {
        [Api]
        public string Email { get; set; }
        [Api]
        public string Password { get; set; }
    }
}
