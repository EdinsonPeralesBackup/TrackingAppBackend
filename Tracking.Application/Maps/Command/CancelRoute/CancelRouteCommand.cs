using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.CancelRoute
{
    public class CancelRouteCommand : IRequest<CancelRouteCommandDTO>
    {
        public int IdUser { get; set; }
    }
}
