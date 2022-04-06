using GameChallenge.Common.Numbers;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using GameChallenge.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GameChallenge.UnitTests
{
    public class PlayerUnitTests
    {
        private readonly IServiceProvider _services = Program.CreateHostBuilder(new string[] { }).Build().Services;

        [Fact]
        public async Task Should_SumAvailablePoints_ShouldMatch()
        {
            //Arrange           
            var myService = _services.GetRequiredService<IPlayerCalculations>();
            var player = new Player()
            {
                PlayerBets = new List<PlayerBet>() {
                        new PlayerBet() { Amount = 100 },
                        new PlayerBet() { Amount = -100 },
                        new PlayerBet() { Amount = 100 } }
            };

            //Act
            int availablePoints = myService.AvailablePoints(player);

            //Assert
            Assert.Equal(100, availablePoints);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(1, 5)]
        [InlineData(1, 2)]
        public async Task Should_ChallengeGetNegativeValueOnLose_ReturnsNegativeValue(int randomNumberGeneratedBySystem, int numberByUser)
        {
            //Arrange           
            var myService = _services.GetRequiredService<IPlayerCalculations>();
        
            //Act
            int pointsGain = myService.Challenge(randomNumberGeneratedBySystem
                ,200, numberByUser, 9);

            //Assert
            Assert.True(pointsGain <= 0);
        }



    }
}
