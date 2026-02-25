using Tracking.Application.Maps.Command.UpdatePoint;

namespace Tracking.Application.Maps.Command.SendSOSSignal
{
    public class RegisterAlert
    {
        public int IdUser { get; set; }
        public string TrackingId { get; set; }
        public Coordinates Coordinate { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
