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

namespace GameChallenge.Core.Services
{
    public class LogService : ILogService
    {
        private IRepository<Log> _logRepository;

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<Log>> GetAll()
        {
            return await _logRepository.ListAsync();
        }

        public async Task<Log> InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", CancellationToken cancellationToken = default)
        {
           
            try
            {

                var log = new Log
                {
                    LogLevel = logLevel,
                    ShortMessage = shortMessage,
                    FullMessage = fullMessage,
                    PageUrl = "",
                    ReferrerUrl = "",
                    CreatedOnUtc = DateTime.UtcNow
                };

                var ss = await _logRepository.AddAsync(log, cancellationToken);
                return ss;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
