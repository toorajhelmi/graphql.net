﻿using System;

namespace Apsy.Common.Api.Auth
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public bool CustomerExists { get; set; }
    }
}
