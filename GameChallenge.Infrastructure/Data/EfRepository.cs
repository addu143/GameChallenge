using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : BaseEntity
    {
        public EfRepository(GameChallengeDBContext dbContext) : base(dbContext)
        {

        }

    }
}
