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
                return BadRequest($"'a' has invalid value '{a}'");
            }

            if (!int.TryParse(b, out int intB))
            {
                return BadRequest($"'b' has invalid value 'abc'");
            }

            return Ok(intA + intB);
        }
    }
}
