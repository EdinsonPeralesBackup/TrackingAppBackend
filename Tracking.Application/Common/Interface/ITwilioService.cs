using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.User.Query.GetUserById;

namespace Tracking.Application.Common.Interface
{
    public interface ITwilioService
    {
        string SendVerificationCode(string phone);
        string CheckVerificationCode(string phone, string code);
        string SendSOS(string phone, GetUserByIdQueryDTO getUserById, Coordinates? coordinates);
    }
}
