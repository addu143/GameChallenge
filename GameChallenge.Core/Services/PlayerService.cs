using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameChallenge.Core.Enum;

namespace GameChallenge.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private IRepository<Player> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISettingService _settingService;

        public PlayerService(IRepository<Player> repository,
            UserManager<ApplicationUser> userManager,
            ISettingService settingService)
        {
            _repository = repository;
            _userManager = userManager;
            _settingService = settingService;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<IdentityResult> CreateCustomAsync(ApplicationUser applicationUser, string password)
        {
            var result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
            {
                //Creating by default 10000 points for a new user:
                Setting defaultPointsForNewUser = await _settingService.GetByNameAsync(SettingsNames.User_DefaultPoints);

                var player = new Player()
                {
                    ApplicationUser = applicationUser,
                    Name = applicationUser.Email                
                };

                await AddPoints(player, Convert.ToInt32(defaultPointsForNewUser.Value), "Default points/money on registration");
                await AddAsync(player);

            }
            return result;
        }

        public async Task AddPoints(Player player, int points, string comment = "", CancellationToken cancellationToken = default)
        {
            if (player.PlayerBets == null) player.PlayerBets = new List<PlayerBet>();
            player.PlayerBets.Add(new PlayerBet()
            {
                Player = player,
                Comment = comment,                
                Amount = points
            });            
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users
                .Where(m => m.Email == email)
                .Include(m => m.Player)
                .ThenInclude(m=> m.PlayerBets)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Player> AddAsync(Player customer, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(customer, cancellationToken);
        }

        public async Task<List<Player>> ListsAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(player, cancellationToken);
        }
    }
}
