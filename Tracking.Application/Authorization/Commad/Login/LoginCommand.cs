using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.Authorization.Commad.Login
{
    public class LoginCommand : IRequest<LoginCommandToken>
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
