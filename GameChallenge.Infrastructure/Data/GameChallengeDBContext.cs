using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Infrastructure.Data
{
    public class GameChallengeDBContext : IdentityDbContext<ApplicationUser>
    {
        public GameChallengeDBContext(DbContextOptions<GameChallengeDBContext> options)
        : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PlayerBet> PlayerBets { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            string defaultString = jAppSettings["ConnectionString"]["DefaultConnection"].Value<string>();
            //optionsBuilder.UseSqlServer(defaultString);
            optionsBuilder.UseSqlite(defaultString);
        }


    }


}
