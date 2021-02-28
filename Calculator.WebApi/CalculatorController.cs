using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi
{
    [ApiController]
    [Route("")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            return Ok(a + b);
        }
    }
}
