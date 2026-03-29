using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.CancelRoute;
using Tracking.Application.Maps.Command.DangerRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetDangerRoute;
using Tracking.Application.Maps.Query.GetTrackingHistory;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface IMapsRepository
    {
        Task<RegisterRouteCommandDTO> RegisterRoute(Route command, int IdUser, int RouteCalibrated);
        Task<UpdatePointCommandDTO> UpdatePoint(UpdatePointCommand command);
        Task<ArriveRouteCommandDTO> ArriveRoute(ArriveRouteCommand command);
        Task<IEnumerable<GetTrackingHistoryQueryDTO>> GetTrackingHistory(GetTrackingHistoryQuery query);
        Task<IEnumerable<CoordinatePointOfRoute>> GetPointOfRoute(int trackingId);
        Task<CancelRouteCommandDTO> CancelRoute(CancelRouteCommand command);
        Task<DangerRouteCommandDTO> DangerRoute(DangerRouteCommand command);
        Task<IEnumerable<GetDangerRouteQueryDTO>> GetDangerRoute(GetDangerRouteQuery query);
    }
}
