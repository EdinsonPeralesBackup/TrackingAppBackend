using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.VerificationCode.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordCommandDTO>
    {
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public int IdUser { get; set; }
    }
}
