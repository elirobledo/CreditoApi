using Microsoft.AspNetCore.Mvc;

namespace CreditoService.Api.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/self")]
        public IActionResult Self() => Ok(new { status = "alive" });

        [HttpGet("/ready")]
        public IActionResult Ready() => Ok(new { status = "ready" });
    }
}
