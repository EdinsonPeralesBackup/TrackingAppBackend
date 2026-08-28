using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.User.Query.GetUserById;

namespace Tracking.Application.Common.Interface
{
    public interface ITwilioService
    {
        Task<string> SendVerificationCodeAsync(string phone);

        Task<bool> CheckVerificationCodeAsync(
            string phone,
            string code);
    }
}