using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Maps.Command.UpdatePoint;

namespace Tracking.Application.Maps.Command.SendSOSSignal
{
    public class SendSOSSignalCommand : IRequest<SendSOSSignalCommandDTO>
    {
        public string TrackingId { get; set; }
        public int UserId { get; set; }
        public Coordinates? Coordinate { get; set; }
    }
}
