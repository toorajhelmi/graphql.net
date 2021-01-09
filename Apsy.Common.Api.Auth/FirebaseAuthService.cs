using Apsy.Common.Api.Core.Helper;
using Firebase.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using static Apsy.Common.Api.Auth.IAuthService;

namespace Apsy.Common.Api.Auth
{
    public class FirebaseAuthService : IAuthService
    {
        public async Task<AuthToken> Signup(AuthConfig authConfig, string email, string password)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(authConfig.ApiKey));
                    var authLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                    var authToken = new AuthToken
                    {
                        Token = authLink.FirebaseToken,
                        ExpiresAt = DateTime.Now.AddSeconds(authLink.ExpiresIn)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var userInfo = handler.ReadJwtToken(authLink.FirebaseToken);
                    userInfo.Payload.TryGetValue("user_id", out object userId);
                    authToken.UserId = userId as string;
                    return authToken;
                }
                catch (FirebaseAuthException e)
                {
                    switch (e.Reason)
                    {
                        case AuthErrorReason.EmailExists: throw new AuthException(SignUpError.EmailAlreadyExists.ToDescription());
                        case AuthErrorReason.MissingEmail: throw new AuthException(SignUpError.MissingEmail.ToDescription());
                        case AuthErrorReason.MissingPassword: throw new AuthException(SignUpError.MissingPassword.ToDescription());
                        case AuthErrorReason.WeakPassword: throw new AuthException(SignUpError.WeakPassword.ToDescription());
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail.ToDescription());
                        case AuthErrorReason.OperationNotAllowed: throw new AuthException(e.Message);
                        case AuthErrorReason.Undefined: throw new AuthException(e.InnerException != null ? e.InnerException.Message : e.Message);
                        default: throw new AuthException(SignUpError.Other.ToDescription());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<AuthToken> EmailLogin(AuthConfig authConfig, string email, string password)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(authConfig.ApiKey));
                    var authLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);

                    var authToken = new AuthToken
                    {
                        Token = authLink.FirebaseToken,
                        ExpiresAt = DateTime.Now.AddSeconds(authLink.ExpiresIn)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var userInfo = handler.ReadJwtToken(authLink.FirebaseToken);
                    userInfo.Payload.TryGetValue("user_id", out object userId);
                    authToken.UserId = userId as string;
                    return authToken;
                }
                catch (FirebaseAuthException e)
                {
                    switch (e.Reason)
                    {
                        case AuthErrorReason.MissingEmail: throw new AuthException(SignUpError.MissingEmail.ToDescription());
                        case AuthErrorReason.MissingPassword: throw new AuthException(SignUpError.MissingPassword.ToDescription());
                        case AuthErrorReason.WrongPassword: throw new AuthException(SignUpError.WrongPassowrd.ToDescription());
                        case AuthErrorReason.UserDisabled: throw new AuthException(SignUpError.DisabledUser.ToDescription());
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail.ToDescription());
                        case AuthErrorReason.Undefined: throw new AuthException(e.InnerException != null ? e.InnerException.Message : e.Message);
                        default: throw new AuthException(SignUpError.Other.ToDescription());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<AuthToken> SocialLogin(AuthConfig authConfig, SocialAuthProvider provider, string accessToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var authType = FirebaseAuthType.Facebook;

                    switch (provider)
                    {
                        case SocialAuthProvider.Facebook: authType = FirebaseAuthType.Facebook; break;
                        case SocialAuthProvider.Google: authType = FirebaseAuthType.Google; break;
                        case SocialAuthProvider.Twitter: authType = FirebaseAuthType.Twitter; break;
                    }

                    var firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(authConfig.ApiKey));
                    var authLink = await firebaseAuthProvider.SignInWithOAuthAsync(authType, accessToken);

                    var authToken = new AuthToken
                    {
                        Token = authLink.FirebaseToken,
                        ExpiresAt = DateTime.Now.AddSeconds(authLink.ExpiresIn)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var userInfo = handler.ReadJwtToken(authLink.FirebaseToken);
                    userInfo.Payload.TryGetValue("user_id", out object userId);
                    authToken.UserId = userId as string;

                    return authToken;
                }
                catch (FirebaseAuthException e)
                {
                    switch (e.Reason)
                    {
                        case AuthErrorReason.MissingEmail: throw new AuthException(SignUpError.MissingEmail.ToDescription());
                        case AuthErrorReason.WrongPassword: throw new AuthException(SignUpError.WrongPassowrd.ToDescription());
                        case AuthErrorReason.UserDisabled: throw new AuthException(SignUpError.DisabledUser.ToDescription());
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail.ToDescription());
                        case AuthErrorReason.Undefined: throw new AuthException(e.InnerException != null ? e.InnerException.Message : e.Message);
                        default: throw new AuthException(SignUpError.Other.ToDescription());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public Task<AuthToken> GoogleLogin(AuthConfig authConfig, string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
