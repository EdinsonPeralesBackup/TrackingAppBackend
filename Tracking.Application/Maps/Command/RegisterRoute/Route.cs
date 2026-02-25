namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class Route
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
        public string EndAddress { get; set; }
        public Location EndLocation { get; set; }
        public string StartAddress { get; set; }
        public Location StartLocation { get; set; }
        public List<Step> Steps { get; set; }
    }
}
