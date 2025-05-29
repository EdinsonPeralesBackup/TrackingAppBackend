using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.VerificationCode.Command.CheckVerificationCode
{
    public class CheckVerificationCodeCommand : IRequest<CheckVerificationCodeCommandDTO>
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int IdUser { get; set; }
    }
}
