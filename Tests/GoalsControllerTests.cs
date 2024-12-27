using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using LifePlanner.Server; // Make sure you are using the namespace containing Program
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Xunit.Abstractions;

namespace LifePlanner.Tests
{
    public class GoalsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public GoalsControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_Goals_Unauthorized_When_No_Auth_Token()
        {
            var response = await _client.GetAsync("/api/users/1/goals");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Get_Goals_Authorized_When_Valid_JWT_Token()
        {
            // Get a valid JWT token from Auth0 or mock one
            var token = GenerateMockToken();
            
            _testOutputHelper.WriteLine(token);
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users/1/goals");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(requestMessage);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        
        private string GenerateMockToken()
        {
            var claims = new[]
            {
                new Claim("sub", "1234567890"),
                new Claim("name", "Test User"),
                new Claim("role", "user"),
                new Claim("aud", "https://dev-skr3fnrj.eu.auth0.com/api/v2/"), // Match your API audience
                new Claim("iss", "https://dev-skr3fnrj.eu.auth0.com"), // Match your Auth0 issuer
            };
            
            var token = new JwtSecurityToken(
                issuer: "https://dev-skr3fnrj.eu.auth0.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mockSecretKeymockSecretKeymockSecretKeymockSecretKey")), SecurityAlgorithms.HmacSha256));  

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}