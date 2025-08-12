using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class RegisterRouteCommand : IRequest<RegisterRouteCommandDTO>
    {
        public int UserId { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
    }
}
