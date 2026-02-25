using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Comand.UpdateUserInfo
{
    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, UpdateUserInfoCommandDTO>
    {
        private readonly ILogger<UpdateUserInfoCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserInfoCommandHandler(
            ILogger<UpdateUserInfoCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<UpdateUserInfoCommandDTO> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.UpdateUserInfo(request);
            return response;
        }
    }
}
