namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class RegisterRouteCommandDTO
    {
        public int TrackingId { get; set; }
        public string Message { get; set; }
        public int CheckpointInterval { get; set; }
        public Route RouteTravel { get; set; }
    }
}
