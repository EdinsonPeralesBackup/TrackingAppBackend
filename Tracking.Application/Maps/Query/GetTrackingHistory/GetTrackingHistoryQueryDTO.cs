using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Query.GetTrackingHistory
{
    public class GetTrackingHistoryQueryDTO
    {
        public double Latitud { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
