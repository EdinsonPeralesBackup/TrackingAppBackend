using MediatR;

namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class SendVerificationCodeCommand : IRequest<SendVerificationCodeCommandDTO>
    {
        public string Phone { get; set; }
    }
}
