using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Authorization.Commad.Login
{
    public class LoginCommandDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Birthday { get; set; }
        public int IdRole { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
