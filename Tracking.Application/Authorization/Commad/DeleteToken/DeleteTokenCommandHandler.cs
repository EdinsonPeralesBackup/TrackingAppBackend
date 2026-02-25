using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Authorization.Commad.DeleteToken
{
    public class DeleteTokenCommandHandler : IRequestHandler<DeleteTokenCommand, DeleteTokenCommandDTO>
    {
        private readonly ILogger<DeleteTokenCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public DeleteTokenCommandHandler(
            ILogger<DeleteTokenCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<DeleteTokenCommandDTO> Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
        {
            this._userRepository.DeleteToken(request);
            return new DeleteTokenCommandDTO()
            {
                Message = "Logout successful"
            };
        }
    }
}
