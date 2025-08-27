using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetTrackingHistory;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MapsController : AbstractController
    {
        [HttpPost]
        [Route("getRoute")]
        [ProducesResponseType(typeof(RegisterRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoute(RegisterRouteCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateLiveCoordinates")]
        [ProducesResponseType(typeof(UpdatePointCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLiveCoordinate(UpdatePointCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("arriveRoute")]
        [ProducesResponseType(typeof(ArriveRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> ArriveRoute(ArriveRouteCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("getTrackingHistory/{IdRoute}")]
        [ProducesResponseType(typeof(RegisterRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrackingHistory(int IdRoute)
        {
            var response = await Mediator.Send(new GetTrackingHistoryQuery()
            {
                RouteId = IdRoute
            });
            return Ok(response);
        }
    }
}
