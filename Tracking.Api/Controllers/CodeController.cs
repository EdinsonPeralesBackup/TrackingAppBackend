using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Services;
using Tracking.Application.VerificationCode.Command.CheckVerificationCode;
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
        [ProducesResponseType(typeof(CurrentUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendCodeVerification(SendVerificationCodeCommand command)
        {
            var response = await this.Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("validationCode")]
        [ProducesResponseType(typeof(CurrentUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidationCodeVerification(CheckVerificationCodeCommand command)
        {
            command.IdUser = Convert.ToInt32(CurrentUser.Id);
            var response = await this.Mediator.Send(command);
            return Ok(response);
        }
    }


}
