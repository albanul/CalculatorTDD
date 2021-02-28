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

        public static string AddEndpoint(string a, string b) => $"/add?a={a}&b={b}";

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
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint("1", "1"));

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        [TestCase(1, -2, -1)]
        public async Task GetAdd_ShouldReturnCorrectValue(int a, int b, int expected)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint(a.ToString(), b.ToString()));

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.Not.Empty);

            int actual = int.Parse(content);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("abc")]
        public async Task GetAdd_ShouldReturnBadRequest_WhenParameterAIsNotAnInteger_WithCorrectErrorMessage(
            string value)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint(value, "1"));

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            string actual = await response.Content.ReadAsStringAsync();
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.Not.Empty);

            Assert.That(actual, Is.EqualTo($"'a' has invalid value '{value}'"));
        }
    }
}
