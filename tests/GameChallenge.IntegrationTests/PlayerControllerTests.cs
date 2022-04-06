using FluentAssertions;
using GameChallenge.Common.Helpers;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using GameChallenge.Web.EnpointModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameChallenge.IntegrationTests
{
    public class PlayerControllerTests : IntergrationBase
    {

        public PlayerControllerTests(IntegrationFixture integrationFixture) : base(integrationFixture)
        {
        }
        
        private async Task RegisterAndAuthentication()
        {
            if (!_fixture.isAlreadyRegisteredAndLoginDefaultUser)
            {
                await RegisterTheUser();
                await AuthenticateAsync();

                _fixture.isAlreadyRegisteredAndLoginDefaultUser = true;
            }
        }

        [Fact]
        public async Task GivenUser_WhenUserIsRegisteringToTheSystem_ThenItShouldBeOk()
        {
            //Arrange
            var request = new PlayerRegisterRequest
            {
                Email = "adnan123@adnan.com",
                Password = "test123"
            };

            //Act
            var response = await Post<PlayerRegisterRequest>("/api/player/register", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenUser_WhenUserAlreadExistToTheSystem_ThenItShouldReturnError()
        {
            //Arrange
            var request = new PlayerRegisterRequest
            {
                Email = "adnan@adnan.com",
                Password = "test123"
            };
            
            //Act
            var response = await Post<PlayerRegisterRequest>("/api/player/register", request);
            response = await Post<PlayerRegisterRequest>("/api/player/register", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenUser_WhenUserEnteredWrongInformationToTheSystem_ThenItShouldReturnError()
        {
            //Arrange
            var request = new PlayerRegisterRequest
            {
                Email = "adnan",
                Password = "te"
            };

            //Act
            var response = await Post<PlayerRegisterRequest>("/api/player/register", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenUser_WhenUserLogInWithIncorrectCredentials_ThenItShouldReturnUnauthorize()
        {
            //Arrange
            var request = new PlayerRegisterRequest
            {
                Email = "adnan@adasdfnan.com",
                Password = "test123123"
            };

            //Act
            var response = await Post<PlayerRegisterRequest>("/api/player/login", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenUser_WhenUserLogInWithCorrectCredentials_ThenItShouldReturnOk()
        {
            //Arrange
            await RegisterTheUser();
            var request = new PlayerRegisterRequest
            {
                Email = "adnan@integration.com",
                Password = "SomePass1234!"
            };

            //Act
            var response = await Post<PlayerRegisterRequest>("/api/player/login", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenChallenge_WhenUserIsDoingChallengeWithCorrectValues_ThenItShouldReturnOK()
        {
            //Arrange:
            await RegisterAndAuthentication();
            var request = new PlayerChallengeRequest()
            {
                Number = 1,
                Points = 3
            };

            //Act
            var response = await Post<PlayerChallengeRequest>("api/player/challenge/", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public async Task GivenChallenge_WhenUserIsDoingChallengeWithInCorrectValues_ThenItShouldReturnError()
        {
            //Arrange:
            await RegisterAndAuthentication();
            var request = new PlayerChallengeRequest()
            {
                Number = -1,
                Points = -1
            };

            //Act
            var response = await Post<PlayerChallengeRequest>("api/player/challenge/", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task GivenChallenge_WhenUserIsDoingChallengeWithNegativePoints_ThenItShouldReturnError()
        {
            //Arrange:
            await RegisterAndAuthentication();
            var request = new PlayerChallengeRequest()
            {
                Number = 1,
                Points = -200
            };

            //Act
            var response = await Post<PlayerChallengeRequest>("api/player/challenge/", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        //[Fact]
        //public async Task Should_Throw_an_erroar_on_playing_with_insufficient_points()
        //{
        //    //Arrange:
        //    await RegisterAndAuthentication();


        //    //Act
        //    var response = await Post<PlayerChallengeRequest>("api/player/challenge/", new PlayerChallengeRequest()
        //    {
        //        Number = 1,
        //        Points = 3
        //    });

        //    //var response = await .HttpClient.PostAsJsonAsync("api/player/challenge/", new PlayerChallengeRequest()
        //    //{
        //    //    Number = 1,
        //    //    Points = 3
        //    //});
        //    var data = await response.Content.ReadFromJsonAsync<ResponseGeneric>();

        //    //Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}
