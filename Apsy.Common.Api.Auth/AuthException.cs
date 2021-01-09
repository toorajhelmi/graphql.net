using System;
using System.ComponentModel;

namespace Apsy.Common.Api.Auth
{
    public enum SignUpError
    {
        [Description("Email already exists")]
        EmailAlreadyExists,

        [Description("Password is weak")]
        WeakPassword,

        [Description("Wrong Email or Password")]
        InvalidEmail,

        [Description("Other")]
        Other,

        [Description("Email is missing")]
        MissingEmail,

        [Description("Password is missing")]
        MissingPassword,

        [Description("Wrong Email or Password")]
        WrongPassowrd,

        [Description("User is disabled")]
        DisabledUser
    }
    public class AuthException : Exception
    {
        public string Error { get; set; }

        public AuthException(string error)
        {
            Error = error;
        }
    }
}
