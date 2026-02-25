using MediatR;

namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class RegisterRouteCommand : IRequest<RegisterRouteCommandDTO>
    {
        public int UserId { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public int RouteCalibrated { get; set; }
    }
}
