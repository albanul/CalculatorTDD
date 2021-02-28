using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi
{
    [ApiController]
    [Route("")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(string a, string b)
        {
            if (!int.TryParse(a, out int intA))
            {
                return BadRequest($"'{nameof(a)}' has invalid value '{a}'");
            }

            if (!int.TryParse(b, out int intB))
            {
                return BadRequest($"'{nameof(b)}' has invalid value '{b}'");
            }

            return Ok(intA + intB);
        }
    }
}
