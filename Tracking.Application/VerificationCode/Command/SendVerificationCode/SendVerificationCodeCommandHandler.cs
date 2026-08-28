using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;

namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler :
        IRequestHandler<SendVerificationCodeCommand, SendVerificationCodeCommandDTO>
    {
        private readonly ILogger<SendVerificationCodeCommandHandler> _logger;
        private readonly ITwilioService _twilioService;

        public SendVerificationCodeCommandHandler(
            ILogger<SendVerificationCodeCommandHandler> logger,
            ITwilioService twilioService)
        {
            _logger = logger;
            _twilioService = twilioService;
        }

        public async Task<SendVerificationCodeCommandDTO> Handle(
            SendVerificationCodeCommand request,
            CancellationToken cancellationToken)
        {
            var status = await _twilioService.SendVerificationCodeAsync(
                request.Phone
            );

            var response = new SendVerificationCodeCommandDTO()
            {
                Message = status == "pending"
                    ? $"Recovery code sent to +51 {request.Phone}"
                    : "Error, code not sent",

                ExpiresIn = status == "pending" ? 360 : 0
            };

            return response;
        }
    }
}