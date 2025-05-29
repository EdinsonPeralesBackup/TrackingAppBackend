using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Filter;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.VerificationCode.Command.ResetPassword;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class UserController : AbstractController
    {
        [HttpDelete]
        [Route("deleteUser")]
        [ProducesResponseType(typeof(DeleteUserCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login()
        {
            var request = new DeleteUserCommand()
            {
                IdUser = Convert.ToInt32(this.CurrentUser.Id)
            };
            var response = await Mediator.Send(request);
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

        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(typeof(LoginCommandToken), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var response = await Mediator.Send(new DeleteTokenCommand()
            {
                IdUser = Convert.ToInt32(CurrentUser.Id)
            });
            return Ok(response);
        }
    }
}
