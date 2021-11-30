using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Calculator.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CalculatorPOC
{
    public class CalculatorControllerTests
    {
        private readonly HttpClient _client;

        private static string AddEndpoint(string a, string b) => $"/add?a={a}&b={b}";

        public CalculatorControllerTests()
        {
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenCalculatorController_WhenGetAddIsCalled_ThenShouldReturnOk()
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint("1", "1"));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(1, -2, -1)]
        public async Task GivenCalculatorController_WhenGetAddIsCalled_ThenShouldReturnCorrectValue(
            int a,
            int b,
            int expected)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint(a.ToString(), b.ToString()));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().NotBeEmpty();

            int actual = int.Parse(content);
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("foo")]
        [InlineData("bar")]
        public async Task
            GivenCalculatorController_WhenGetAddIsCalledAndParameterAIsNotAnInteger_ThenShouldReturnBadRequest(
                string value)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint(value, "1"));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            string actual = await response.Content.ReadAsStringAsync();
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();

            actual.Should().Be($"'a' has invalid value '{value}'");
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("foo")]
        [InlineData("bar")]
        public async Task
            GivenCalculatorController_WhenGetAddIsCalledAndParameterBIsNotAnInteger_ThenShouldReturnBadRequest(
                string value)
        {
            // act
            HttpResponseMessage response = await _client.GetAsync(AddEndpoint("1", value));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            string actual = await response.Content.ReadAsStringAsync();
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();

            actual.Should().Be($"'b' has invalid value '{value}'");
        }
    }
}