using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EncryptController : AbstractController
    {
        [HttpGet]
        [Route("encrypt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Encrypt(string text)
        {
            var response = this.Cryptography.Encrypt(text);
            return Ok(response);
        }
    }
}
