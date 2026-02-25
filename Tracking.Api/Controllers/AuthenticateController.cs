using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Filter;
using Tracking.Api.Services;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class AuthenticateController : AbstractController
    {
        [HttpGet]
        [Authorize]
        [Route("me")]
        [ProducesResponseType(typeof(CurrentUser), StatusCodes.Status200OK)]
        public IActionResult UserInfo()
        {
            return Ok(CurrentUser);
        }
    }
}
