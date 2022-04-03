using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GameChallenge.Web.StartupHelpers
{
    public class WebWorkerContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPlayerService _playerService;
        private ApplicationUser _cachedPlayer;

        public WebWorkerContext(IPlayerService playerService,
            IHttpContextAccessor httpContextAccessor)
        {
            this._playerService = playerService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public virtual ApplicationUser CurrentPlayer
        {
            get
            {
                if (_cachedPlayer != null)
                    return _cachedPlayer;

                //Getting data from token provided:
                var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
                _cachedPlayer = _playerService.FindByEmailAsync(email).Result;

                return _cachedPlayer;

            }
            set
            {
                _cachedPlayer = value;
            }
        }

    }
}
