using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Maps.Command.UpdatePoint;

namespace Tracking.Application.Maps.Query.GetDangerRoute
{
    public class GetDangerRouteQueryDTO
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
