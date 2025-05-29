using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.VerificationCode.Command.CheckVerificationCode
{
    public class CheckVerificationCodeCommandDTO
    {
        public string Message { get; set; }
        public string VerificationCode { get; set; }
    }
}
