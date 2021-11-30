using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Calculator.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CalculatorPOC
{
    public class HealthControllerTests
    {
        [Fact]
        public async Task GivenHealthController_WhenHealthActionIsCalled_ThenOkShouldBeReturned()
        {
            // assert
            var factory = new WebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            // act
            HttpResponseMessage response = await client.GetAsync("/health");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}