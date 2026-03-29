using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Tracking.Api.Filter;
using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.CancelRoute;
using Tracking.Application.Maps.Command.DangerRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetDangerRoute;
using Tracking.Application.Maps.Query.GetTrackingHistory;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizationFilter))]
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
        [Route("finishRoute")]
        [ProducesResponseType(typeof(ArriveRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> ArriveRoute(ArriveRouteCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("cancelRoute")]
        [ProducesResponseType(typeof(CancelRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelRoute()
        {
            var response = await Mediator.Send(new CancelRouteCommand()
            {
                IdUser = Convert.ToInt32(this.CurrentUser.Id)
            });
            return Ok(response);
        }

        [HttpPost]
        [Route("dangerRoute")]
        [ProducesResponseType(typeof(CancelRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> DangerRoute(DangerRouteCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("getDangerRoute/{trackingId}")]
        [ProducesResponseType(typeof(GetDangerRouteQueryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> DangerRoute(string trackingId)
        {
            var response = await Mediator.Send(new GetDangerRouteQuery()
            {
                TrackingId = trackingId
            });
            return Ok(response);
        }

        [HttpPost]
        [Route("getTrackingHistory/{isRutaActual}")]
        [ProducesResponseType(typeof(RegisterRouteCommandDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrackingHistory(bool isRutaActual)
        {
            var response = await Mediator.Send(new GetTrackingHistoryQuery()
            {
                IdUser = Convert.ToInt32(this.CurrentUser.Id),
                EsRutaActual = isRutaActual
            });
            return Ok(response);
        }
    }
}
