using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.VerificationCode.Command.ResetPassword
{
    public class ResetPasswordCommandHandler :
        IRequestHandler<ResetPasswordCommand, ResetPasswordCommandDTO>
    {
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITwilioService _twilioService;

        public ResetPasswordCommandHandler(
            ILogger<ResetPasswordCommandHandler> logger,
            IUserRepository userRepository,
            ITwilioService twilioService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _twilioService = twilioService;
        }

        public async Task<ResetPasswordCommandDTO> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var isApproved =
                await _twilioService.CheckVerificationCodeAsync(
                    request.Phone,
                    request.Code);

            if (!isApproved)
            {
                return new ResetPasswordCommandDTO()
                {
                    Message = "Update password failed."
                };
            }

            var response =
                await _userRepository.ResetPassword(request);

            return response;
        }
    }
}