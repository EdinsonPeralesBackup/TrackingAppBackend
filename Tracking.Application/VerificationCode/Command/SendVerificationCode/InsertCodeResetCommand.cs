using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class InsertCodeResetCommand
    {
        public string Code { get; set; }
        public string Phone{ get; set; }
    }
}