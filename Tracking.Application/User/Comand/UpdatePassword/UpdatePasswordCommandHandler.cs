using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Comand.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCommandDTO>
    {
        private readonly ILogger<UpdatePasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdatePasswordCommandHandler(
            ILogger<UpdatePasswordCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<UpdatePasswordCommandDTO> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
