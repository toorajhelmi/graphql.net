using Apsy.Common.Api.Core.Graph;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apsy.Common.Api.Auth
{
    public class UserBase
    {
        public string FirebaseUserId { get; set; }
        [Api]
        public string Email { get; set; }
        [Api]
        [NotMapped]
        public string Password { get; set; }
    }
}
