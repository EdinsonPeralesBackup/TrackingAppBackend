namespace Tracking.Application.Maps.Command.UpdatePoint
{
    public class UpdatePointCommandDTO
    {
        public string Status { get; set; }
        public int NextCheckIn { get; set; }
        public Coordinates LastValidPoint { get; set; }
        public double DeviationRadius { get; set; }
    }
}
