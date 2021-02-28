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
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task GetAdd_ShouldReturnOk()
        {
            // act
            HttpResponseMessage response = await _client.GetAsync("/add?a=1&b=1");

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        [TestCase(1, -2, -1)]
        public async Task GetAdd_ShouldReturnCorrectValue(int a, int b, int expected)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync($"/add?a={a}&b={b}");

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);

            int actual = int.Parse(content);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
