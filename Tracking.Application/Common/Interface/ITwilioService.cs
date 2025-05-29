using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Common.Interface
{
    public interface ITwilioService
    {
        string SendVerificationCode(string phone);
        string CheckVerificationCode(string phone, string code);
    }
}
