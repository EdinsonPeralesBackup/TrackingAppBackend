using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.User.Query.GetUserById;

namespace Tracking.Application.User.Comand.ChangePassword
{
    public class ChangePasswordCommandHandler :
        IRequestHandler<ChangePasswordCommand, ChangePasswordCommandDTO>
    {
        private readonly ILogger<ChangePasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITwilioService _twilioService;

        public ChangePasswordCommandHandler(
            ILogger<ChangePasswordCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository,
            ITwilioService twilioService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _twilioService = twilioService;
        }

        public async Task<ChangePasswordCommandDTO> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(
                new GetUserByIdQuery()
                {
                    Id = request.IdUser
                });

            if (user == null || string.IsNullOrWhiteSpace(user.Phone))
            {
                return new ChangePasswordCommandDTO()
                {
                    Message = "Update password failed."
                };
            }

            var isApproved =
                await _twilioService.CheckVerificationCodeAsync(
                    user.Phone,
                    request.CodeVerifacion);

            if (!isApproved)
            {
                return new ChangePasswordCommandDTO()
                {
                    Message = "Update password failed."
                };
            }

            var response =
                await _userRepository.ChangePassword(request);

            return response;
        }
    }
}