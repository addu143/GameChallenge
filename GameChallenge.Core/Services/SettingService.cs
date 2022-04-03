using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Services
{
    public class SettingService : ISettingService
    {
        private IRepository<Setting> _repository;

        public SettingService(IRepository<Setting> repository)
        {
            _repository = repository;
        }
        public Task<Setting> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _repository.Table.Where(m => m.Name == name).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<Setting>> ListOfSettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }
    }
}
