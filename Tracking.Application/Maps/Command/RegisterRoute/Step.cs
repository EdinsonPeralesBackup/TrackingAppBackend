namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class Step
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
        public Location Start_location { get; set; }
        public Location End_location { get; set; }
        public string Html_instructions { get; set; }
        public Polyline Polyline { get; set; }
        public string Travel_mode { get; set; }
    }

    public class Distance
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class Duration
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class Polyline
    {
        public string Points { get; set; }
    }

}
