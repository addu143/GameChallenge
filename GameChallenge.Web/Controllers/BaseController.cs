using Microsoft.AspNetCore.Mvc;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using System.Security.Claims;

namespace GameChallenge.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public BaseController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        protected ApplicationUser GetCurrentUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            return _playerService.FindByEmailAsync(email).Result;

        }
    }
}
