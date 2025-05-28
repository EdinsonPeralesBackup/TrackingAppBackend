using Tracking.Application.Common.Interface;

namespace Tracking.Api.Services
{
    public class CurrentUser : ICurrentUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
    }
}
