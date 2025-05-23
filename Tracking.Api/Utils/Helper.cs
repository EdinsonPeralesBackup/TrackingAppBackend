namespace Tracking.Api.Utils
{
    public static class Helper
    {
        public static DateTime FechaActualPeru()
        {
            DateTime FechaActual = DateTime.UtcNow;
            TimeZoneInfo ZonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime FechaActualPeru = TimeZoneInfo.ConvertTimeFromUtc(FechaActual, ZonaHoraria);
            return FechaActualPeru;
        }
    }
}
