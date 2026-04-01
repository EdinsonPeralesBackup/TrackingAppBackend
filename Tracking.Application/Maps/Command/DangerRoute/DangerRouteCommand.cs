using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.DangerRoute
{
    public class DangerRouteCommand : IRequest<DangerRouteCommandDTO>
    {
        public int TrackingId { get; set; }
        public DateTime? DateDanger { get; set; }
    }
}
