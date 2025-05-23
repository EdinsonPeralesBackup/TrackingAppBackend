using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Common.Interface
{
    public interface ICurrentUser
    {
        string Identifier { get; set; }
        string Nombres { get; set; }
        string ApellidoPaterno { get; set; }
        string ApellidoMaterno { get; set; }
        string NombreCompleto { get; set; }
        string RolId { get; set; }
        string Rol { get; set; }
    }
}
