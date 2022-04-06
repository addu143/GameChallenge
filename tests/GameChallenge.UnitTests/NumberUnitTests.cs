using GameChallenge.Common.Numbers;
using GameChallenge.Core.DBEntities;
using GameChallenge.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GameChallenge.UnitTests
{
    public class NumberUnitTests
    {
        private readonly IServiceProvider _services = Program.CreateHostBuilder(new string[] { }).Build().Services;

        [Fact]
        public async Task Should_SystemGenerateRandomNumber_InRange()
        {
            //Arrange           
            var numberService = _services.GetRequiredService<INumberHelper>();

            //Act
            int randomNumber = numberService.GenerateRandomNumber(0, 9);

            //Assert
            Assert.InRange<int>(randomNumber, 0, 9);
        }

    }
}
