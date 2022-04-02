using Microsoft.AspNetCore.Mvc;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using System.Security.Claims;

namespace GameChallenge.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public BaseController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        protected ApplicationUser GetCurrentUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            return _customerService.FindByEmailAsync(email).Result;

        }
    }
}
