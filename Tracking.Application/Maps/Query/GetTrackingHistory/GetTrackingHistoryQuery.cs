using MediatR;

namespace Tracking.Application.Maps.Query.GetTrackingHistory
{
    public class GetTrackingHistoryQuery : IRequest<IEnumerable<GetTrackingHistoryQueryDTO>>
    {
        public int IdUser { get; set; }
        public bool EsRutaActual { get; set; }
    }
}
