using Tracking.Application.Common.Dtos;

namespace Tracking.Application.Common.Settings
{
    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public string ApplicationDisplayName { get; set; }
        public string ApplicationId { get; set; }
        public long LongRequestTimeMilliseconds { get; set; }
        public long SlidingExpirationCacheTimeSeconds { get; set; }
        public MensajeUsuarioDTO GeneralErrorMessage { get; set; }
    }
}
