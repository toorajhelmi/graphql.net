using System.Threading.Tasks;

namespace Apsy.Elemental.Core.Identity
{
    public interface IAuthService
    {
        public enum SocialAuthProvider
        {
            Facebook,
            Google,
            Twitter,
            LinkedIn
        }

        Task<AuthToken> Signup(AuthConfig authConfig, string email, string password);
        Task<AuthToken> EmailLogin(AuthConfig authConfig, string email, string password);
        Task<AuthToken> SocialLogin(AuthConfig authConfig, SocialAuthProvider provider, string accessToken);
    }
}