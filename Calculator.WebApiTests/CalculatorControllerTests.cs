using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Calculator.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CalculatorPOC
{
    public class CalculatorControllerTests
    {
        [Test]
        public async Task GetAdd_ShouldReturnOk()
        {
            // arrange
            var factory = new WebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            // act
            HttpResponseMessage response = await client.GetAsync("/add?a=1&b=1");

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
