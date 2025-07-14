using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Filter;
using Tracking.Api.Services;
using Tracking.Application.VerificationCode.Command.CheckVerificationCode;
using Tracking.Application.VerificationCode.Command.SendVerificationCode;

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
        [ProducesResponseType(typeof(CurrentUser) ,StatusCodes.Status200OK)]
        public IActionResult UserInfo()
        {
            return Ok(CurrentUser);
        }
    }
}
