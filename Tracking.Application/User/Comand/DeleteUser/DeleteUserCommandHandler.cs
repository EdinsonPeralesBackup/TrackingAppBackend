using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Comand.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandDTO>
    {
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(
            ILogger<DeleteUserCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<DeleteUserCommandDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.DeleteUser(request);
            return response;
        }
    }
}
