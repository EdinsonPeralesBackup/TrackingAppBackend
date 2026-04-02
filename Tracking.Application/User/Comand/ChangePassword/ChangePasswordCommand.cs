using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.User.Comand.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandDTO>
    {
        public string CodeVerifacion { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public int IdUser { get; set; }
    }
}
