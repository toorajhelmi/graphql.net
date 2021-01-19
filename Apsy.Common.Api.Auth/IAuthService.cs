using System.Threading.Tasks;

namespace Apsy.Common.Api.Auth
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
        Task ResetPassword(AuthConfig authConfig, string email);
        Task<AuthToken> ChangePassword(AuthConfig authConfig, string token, string newPassoword);
    }
}