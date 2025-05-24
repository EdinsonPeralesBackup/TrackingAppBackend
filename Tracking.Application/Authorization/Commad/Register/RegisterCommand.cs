using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.Authorization.Commad.Register
{
    public class RegisterCommand : IRequest<RegisterCommandDTO>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Birthday { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
    }
}
