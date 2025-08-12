using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MapsController : AbstractController
    {
        [HttpPost]
        [Route("getRoute")]
        [ProducesResponseType(typeof(RegisterRouteCommand), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoute(RegisterRouteCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateLiveCoordinates")]
        [ProducesResponseType(typeof(UpdatePointCommand), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLiveCoordinate(UpdatePointCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
