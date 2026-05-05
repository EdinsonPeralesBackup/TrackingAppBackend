using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Filter;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.User.Comand.ChangePassword;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.User.Comand.UpdateUserInfo;
using Tracking.Application.User.Query.GetUserById;
using Tracking.Application.User.Query.GetUsers;
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
        public async Task<IActionResult> DeleteUser()
        {
            var request = new DeleteUserCommand()
            {
                IdUser = Convert.ToInt32(this.CurrentUser.Id)
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("getUser")]
        [ProducesResponseType(typeof(GetUsersQueryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(int page, int limit)
        {
            var request = new GetUsersQuery()
            {
                Page = page,
                Limit = limit,
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        [ProducesResponseType(typeof(GetUsersQueryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var request = new GetUserByIdQuery()
            {
                Id = id
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteUserById/{id}")]
        [ProducesResponseType(typeof(DeleteUserCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var request = new DeleteUserCommand()
            {
                IdUser = id
            };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("changePassword")]
        [ProducesResponseType(typeof(ChangePasswordCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> changePassword(ChangePasswordCommand request)
        {
            request.IdUser = Convert.ToInt32(this.CurrentUser.Id);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateUserInfo")]
        [ProducesResponseType(typeof(UpdateUserInfoCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoCommand request)
        {
            request.Id = Convert.ToInt32(CurrentUser.Id);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateUserInfoById")]
        [ProducesResponseType(typeof(UpdateUserInfoCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfoById(UpdateUserInfoCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(typeof(DeleteTokenCommandDTO), StatusCodes.Status200OK)]
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
