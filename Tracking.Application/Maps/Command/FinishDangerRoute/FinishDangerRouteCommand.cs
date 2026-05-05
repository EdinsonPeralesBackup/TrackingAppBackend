using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.FinishDangerRoute
{
    public class FinishDangerRouteCommand : IRequest<FinishDangerRouteCommandDTO>
    {
        public string TrackingId { get; set; }
        public int IdUser { get; set; }
    }
}
