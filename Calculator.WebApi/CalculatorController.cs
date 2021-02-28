using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi
{
    [ApiController]
    [Route("")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add()
        {
            return Ok(2);
        }
    }
}
