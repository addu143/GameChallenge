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
using Xunit;

namespace GameChallenge.IntegrationTests
{
    //For more information about Iclassfixture : https://xunit.net/docs/shared-context
    public class IntergrationBase : IClassFixture<IntegrationFixture>
    {
        protected IntegrationFixture _fixture;

        public IntergrationBase(IntegrationFixture integrationFixture)
        {
            _fixture = integrationFixture;
        }

        public async Task<HttpResponseMessage> Post<T>(string url, T content) where T : class
        {
            return await _fixture.HttpClient.PostAsync(url, ToStringContent(content));
        }


        private static StringContent ToStringContent<T>(T content) where T : class
        {
            return new StringContent(System.Text.Json.JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }

        protected async Task AuthenticateAsync(string email = "adnan@integration.com", string password = "SomePass1234!")
        {
            string token = await GetJwtAsync(email, password);
            _fixture.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        protected async Task RegisterTheUser(string email = "adnan@integration.com", string password = "SomePass1234!")
        {
            await _fixture.HttpClient.PostAsJsonAsync("/api/player/register", new {
                Email = email,
                Password = password
            });
        }

        private async Task<string> GetJwtAsync(string email, string password)
        {
            var response = await _fixture.HttpClient.PostAsJsonAsync("/api/player/login", new PlayerLoginRequest
            {
                Email = email,
                Password = password
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<ResponseGeneric>();
            string result = Convert.ToString(registrationResponse.Result);
            var generatedcsResponce = JsonConvert.DeserializeObject<PlayerLoginResponse>(result);
            return generatedcsResponce.Token;

        }
    }
}
