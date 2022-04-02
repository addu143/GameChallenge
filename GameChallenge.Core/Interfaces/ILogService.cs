using Microsoft.Extensions.Logging;
using GameChallenge.Core.DBEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LogLevel = GameChallenge.Core.DBEntities.LogLevel;

namespace GameChallenge.Core.Interfaces
{
    public interface ILogService
    {
        Task<Log> InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", CancellationToken cancellationToken = default);
        Task<List<Log>> GetAll();

    }
}