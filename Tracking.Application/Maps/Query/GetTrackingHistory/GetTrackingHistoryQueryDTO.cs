namespace Tracking.Application.Maps.Query.GetTrackingHistory
{
    public class GetTrackingHistoryQueryDTO
    {
        public int IdRoute { get; set; }
        public string Origen { get; set; }
        public double OrigenLatitud { get; set; }
        public double OrigenLongitude { get; set; }
        public string Destination { get; set; }
        public double DestinationLatitud { get; set; }
        public double DestinationLongitude { get; set; }
        public DateTime Timestamp { get; set; }
        public string State { get; set; }
    }
}
