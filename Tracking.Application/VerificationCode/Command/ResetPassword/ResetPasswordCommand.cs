using MediatR;

namespace Tracking.Application.VerificationCode.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordCommandDTO>
    {
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public string Phone { get; set; }
    }
}
