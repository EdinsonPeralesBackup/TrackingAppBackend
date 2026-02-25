using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.User.Query.GetUserById;

namespace Tracking.Application.Common.Interface
{
    public interface ITwilioService
    {
        string SendVerificationCode(string phone, string message);
        string SendSOS(string phone, GetUserByIdQueryDTO getUserById, Coordinates? coordinates, string trackingId);
    }
}
