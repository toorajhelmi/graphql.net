using System.Security.Policy;

namespace Apsy.Elemental.Core.Identity
{
    public class AuthConfig
    { 
        public string ApiKey { get; set; } 
        public string SignupUri { get; set; }
        public string LoginUri { get; set; }
    }
}
