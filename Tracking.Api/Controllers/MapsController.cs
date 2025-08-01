using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Maps.Command.ObtenerRuta;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MapsController : AbstractController
    {
        [HttpPost]
        [Route("getRoute")]
        [ProducesResponseType(typeof(ObtenerRutaCommand), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoute(ObtenerRutaCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
