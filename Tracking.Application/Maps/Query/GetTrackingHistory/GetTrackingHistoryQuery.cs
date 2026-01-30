using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Query.GetTrackingHistory
{
    public class GetTrackingHistoryQuery : IRequest<IEnumerable<GetTrackingHistoryQueryDTO>>
    {
        public int IdUser { get; set; }
        public bool EsRutaActual{ get; set; }
    }
}
