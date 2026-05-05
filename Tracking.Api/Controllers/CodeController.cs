using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Services;
using Tracking.Application.TrustedContacts.Command.RegisterVisit;
using Tracking.Application.VerificationCode.Command.SendVerificationCode;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CodeController : AbstractController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("sendCode")]
        [ProducesResponseType(typeof(SendVerificationCodeCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendCodeVerification(SendVerificationCodeCommand command)
        {
            var response = await this.Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("registerVisit/{trackingId}")]
        [ProducesResponseType(typeof(RegisterVisitCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterVisit(string trackingId)
        {
            var response = await Mediator.Send(new RegisterVisitCommand()
            {
                TrackingId = trackingId
            });
            return Ok();
        }
    }
}
