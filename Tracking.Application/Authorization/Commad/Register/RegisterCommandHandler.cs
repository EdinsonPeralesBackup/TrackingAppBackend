using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Authorization.Commad.Register
{
    public class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, RegisterCommandDTO>
    {
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly ITwilioService _twilioService;

        public RegisterCommandHandler(
            ILogger<RegisterCommandHandler> logger,
            IMapper mapper,
            IAuthorizationRepository authorizationRepository,
            ITwilioService twilioService)
        {
            _logger = logger;
            _mapper = mapper;
            _authorizationRepository = authorizationRepository;
            _twilioService = twilioService;
        }

        public async Task<RegisterCommandDTO> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            // Evita llamar a Twilio si Flutter no envía el número.
            if (string.IsNullOrWhiteSpace(request.Phonenumber))
            {
                return new RegisterCommandDTO()
                {
                    UserId = 0,
                    Message = "Phone number is required."
                };
            }

            // Evita llamar a Twilio si no se envía el código.
            if (string.IsNullOrWhiteSpace(request.CodeVerification))
            {
                return new RegisterCommandDTO()
                {
                    UserId = 0,
                    Message = "Verification code is required."
                };
            }

            var isApproved =
                await _twilioService.CheckVerificationCodeAsync(
                    request.Phonenumber,
                    request.CodeVerification);

            if (!isApproved)
            {
                return new RegisterCommandDTO()
                {
                    UserId = 0,
                    Message = "An error occurred during user registration."
                };
            }

            var response =
                await _authorizationRepository.Register(request);

            return response;
        }
    }
}