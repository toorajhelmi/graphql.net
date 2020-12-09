using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using static Apsy.Elemental.Core.Identity.IAuthService;

namespace Apsy.Elemental.Core.Identity
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
                        case AuthErrorReason.EmailExists: throw new AuthException(SignUpError.EmailAlreadyExists);
                        case AuthErrorReason.WeakPassword: throw new AuthException(SignUpError.WeakPassword);
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail);
                        default: throw new AuthException(SignUpError.Other);
                    }
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
                        case AuthErrorReason.MissingEmail: throw new AuthException(SignUpError.MissingEmail);
                        case AuthErrorReason.WrongPassword: throw new AuthException(SignUpError.WrongPassowrd);
                        case AuthErrorReason.UserDisabled: throw new AuthException(SignUpError.DisabledUser);
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail);
                        default: throw new AuthException(SignUpError.Other);
                    }
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
                        case AuthErrorReason.MissingEmail: throw new AuthException(SignUpError.MissingEmail);
                        case AuthErrorReason.WrongPassword: throw new AuthException(SignUpError.WrongPassowrd);
                        case AuthErrorReason.UserDisabled: throw new AuthException(SignUpError.DisabledUser);
                        case AuthErrorReason.InvalidEmailAddress: throw new AuthException(SignUpError.InvalidEmail);
                        default: throw new AuthException(SignUpError.Other);
                    }
                }
            }
        }

        public Task<AuthToken> GoogleLogin(AuthConfig authConfig, string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
