using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface IMapsRepository
    {
        Task<RegisterRouteCommandDTO> RegisterRoute(Route command, int IdUser);
        Task<UpdatePointCommandDTO> UpdatePoint(UpdatePointCommand command);
    }
}
