using Tracking.Application.Maps.Command.ObtenerRuta;

namespace Tracking.Application.Common.Interface
{
    public interface IMapsServices
    {
        Task<string> ObtenerRuta(RegisterRouteCommand command);
    }
}
