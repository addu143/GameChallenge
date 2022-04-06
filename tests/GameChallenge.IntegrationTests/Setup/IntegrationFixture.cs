using GameChallenge.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GameChallenge.Infrastructure.Data;
using GameChallenge.Web.EnpointModel;
using GameChallenge.Common.Helpers;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GameChallenge.Core.Interfaces;

namespace GameChallenge.IntegrationTests
{
    public class IntegrationFixture : IDisposable
    {
        protected GameChallengeDBContext _dbContext;
        private readonly WebApplicationFactory<Startup> _factory;
        public HttpClient HttpClient;
        public bool isAlreadyRegisteredAndLoginDefaultUser = false;

        public IntegrationFixture()
        {
            _factory = new WebApplicationFactory<Startup>()
                                .WithWebHostBuilder(builder =>
                                {
                                    builder.ConfigureServices(services =>
                                    {
                                        services.RemoveAll(typeof(GameChallengeDBContext));
                                        services.AddDbContext<GameChallengeDBContext>(options => { options.UseInMemoryDatabase("TestDb"); });

                                        var serviceProvider = services.BuildServiceProvider();
                                        using var scope = serviceProvider.CreateScope();
                                        _dbContext = scope.ServiceProvider.GetRequiredService<GameChallengeDBContext>();

                                        // Ensure the database is created.
                                        _dbContext.Database.EnsureDeleted();
                                        //db.Database.EnsureCreated();          
                                    });
                                });

            HttpClient = _factory.CreateClient();
        }
        public void Dispose()
        {
            
        }        
    }
}
