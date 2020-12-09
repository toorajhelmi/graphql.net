using Apsy.Elemental.Core.Identity;
using Apsy.Elemental.Example.Web.Models;
using Apsy.Elemental.Example.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Apsy.Elemental.Core.Identity.IAuthService;

namespace Apsy.Elemental.Example.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration, DataContext dataContext, IAuthService authService)
        {
            this.configuration = configuration;
            this.dataContext = dataContext;
            this.authService = authService;
        }

        //[HttpPost("signup")]
        //public async Task<ActionResult<AuthToken>> Signup(User user)
        //{
        //    var authConfig = configuration.GetSection("AuthConfig").Get<AuthConfig>();
        //    var authToken = await authService.Signup(authConfig, user.Email, user.Password);
        //    return authToken;
        //}

        [HttpPost("emaillogin")]
        public async Task<ActionResult<AuthToken>> EmailLogin(User user)
        {
            var authConfig = configuration.GetSection("AuthConfig").Get<AuthConfig>();
            var authToken = await authService.EmailLogin(authConfig, user.Email, user.Password);
            return authToken;
        }

        [HttpGet("sociallogin")]
        public async Task<ActionResult<AuthToken>> SocialLogin(SocialAuthProvider provider, string accessToken)
        {
            var authConfig = configuration.GetSection("AuthConfig").Get<AuthConfig>();
            var authToken = await authService.SocialLogin(authConfig, IAuthService.SocialAuthProvider.Facebook, accessToken);
            authToken.CustomerExists = dataContext.Customer.Any(c => c.UserId == authToken.UserId);

            return authToken;  
        }
    }
}
