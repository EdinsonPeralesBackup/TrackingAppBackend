using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.User.Comand.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<UpdatePasswordCommandDTO>
    {
        public string ResetCode { get; set; }
        public string NewPassword { get; set; }
        public int IdUser { get; set; }
    }
}
