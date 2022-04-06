using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameChallenge.Core.Enum;

namespace GameChallenge.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasData(
                new Setting { Id = 1, Name = SettingsNames.Challenge_RandomNumberMin, Value = "0", Description = "Random minimum number in a challenge" },
                new Setting { Id = 2, Name = SettingsNames.Challenge_RandomNumberMax, Value = "9", Description = "Random maximum number in a challenge" },
                new Setting { Id = 3, Name = SettingsNames.Challenge_RewardHowManyTimes, Value = "9", Description = "e.g. If he is right, he gets 9 times his stake as a prize" },
                new Setting { Id = 4, Name = SettingsNames.User_DefaultPoints, Value = "10000", Description = "Default points for a new user." }

            );
        }
    }
}
