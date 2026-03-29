using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Query.GetDangerRoute
{
    public class GetDangerRouteQuery : IRequest<IEnumerable<GetDangerRouteQueryDTO>>
    {
        public string TrackingId { get; set; }
    }
}