using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasData(
                new Setting { Id = 1, Name = "RandomNumberMin", Value = "0", Description = "Random minimum number in a challenge" },
                new Setting { Id = 2, Name = "RandomNumberMax", Value = "9", Description = "Random maximum number in a challenge" },
                new Setting { Id = 3, Name = "RewardHowManyTimes", Value = "9", Description = "e.g. If he is right, he gets 9 times his stake as a prize" }
            );
        }
    }
}
