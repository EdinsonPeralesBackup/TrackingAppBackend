using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class ObtenerRutaCommandDTO
    {
        public string TrackingId { get; set; }
        public string Message { get; set; }
        public int CheckpointInterval { get; set; }
        public Route RouteTravel { get; set; }
    }
}
