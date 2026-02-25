using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetTrackingHistory;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface IMapsRepository
    {
        Task<RegisterRouteCommandDTO> RegisterRoute(Route command, int IdUser, int RouteCalibrated);
        Task<UpdatePointCommandDTO> UpdatePoint(UpdatePointCommand command);
        Task<ArriveRouteCommandDTO> ArriveRoute(ArriveRouteCommand command);
        Task<IEnumerable<GetTrackingHistoryQueryDTO>> GetTrustedContacts(GetTrackingHistoryQuery query);
    }
}
