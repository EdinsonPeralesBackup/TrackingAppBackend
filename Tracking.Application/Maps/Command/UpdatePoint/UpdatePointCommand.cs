using MediatR;

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
