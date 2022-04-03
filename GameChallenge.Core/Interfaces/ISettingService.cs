using GameChallenge.Core.DBEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface ISettingService
    {
        Task<Setting> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<Setting>> ListOfSettingsAsync(CancellationToken cancellationToken = default);
    }
}