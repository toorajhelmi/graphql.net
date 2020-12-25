using System;

namespace Apsy.Common.Api.Identity
{
    public enum SignUpError
    {
        EmailAlreadyExists,
        WeakPassword,
        InvalidEmail,
        Other,
        MissingEmail,
        WrongPassowrd,
        DisabledUser
    }

    public class AuthException : Exception
    {
        public SignUpError Error { get; set; }

        public AuthException(SignUpError error) 
        {
            Error = error;
        }
    }
}
