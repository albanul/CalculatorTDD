using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi
{
    [ApiController]
    [Route("")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(string a, int b)
        {
            if (!int.TryParse(a, out int intA))
            {
                return BadRequest($"'a' has invalid value '{a}'");
            }

            return Ok(intA + b);
        }
    }
}
