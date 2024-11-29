using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Redirector.Controllers
{
    [ApiController]
    public class SmartLinkController : ControllerBase
    {
        [HttpGet("{link}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute] string link)
        {
            return StatusCode(429);
        }
    }
}
