using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Authorization.Commad.Register;
using Tracking.Application.VerificationCode.Command.ResetPassword;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : AbstractController
    {
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginCommandToken), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("deleteToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteToken(DeleteTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("resetPassword")]
        [ProducesResponseType(typeof(ResetPasswordCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
