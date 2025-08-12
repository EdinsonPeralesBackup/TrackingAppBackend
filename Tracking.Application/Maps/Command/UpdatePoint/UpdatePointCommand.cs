using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.UpdatePoint
{
    public class UpdatePointCommand : IRequest<UpdatePointCommandDTO>
    {
        public int TrackingId { get; set; }
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
