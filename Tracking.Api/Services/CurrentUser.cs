using Tracking.Application.Common.Interface;

namespace Tracking.Api.Services
{
    public class CurrentUser : ICurrentUser
    {
        public string Identifier { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string RolId { get; set; }
        public string Rol { get; set; }
    }
}
