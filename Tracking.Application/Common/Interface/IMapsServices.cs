using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Maps.Command.ObtenerRuta;

namespace Tracking.Application.Common.Interface
{
    public interface IMapsServices
    {
        Task<string> ObtenerRuta(ObtenerRutaCommand command);
    }
}
