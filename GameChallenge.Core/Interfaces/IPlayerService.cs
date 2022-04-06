using Microsoft.AspNetCore.Identity;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);

        Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password);

        Task<IdentityResult> CreateCustomAsync(ApplicationUser applicationUser, string password);

        Task<Player> AddAsync(Player player, CancellationToken cancellationToken = default);

        Task UpdateAsync(Player player, CancellationToken cancellationToken = default);

        Task<List<Player>> ListsAsync(CancellationToken cancellationToken = default);

        Task AddPoints(Player player, int points, string comment = "", CancellationToken cancellationToken = default);


    }
}
