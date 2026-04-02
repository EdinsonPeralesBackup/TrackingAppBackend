using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Comand.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandDTO>
    {
        private readonly ILogger<ChangePasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(
            ILogger<ChangePasswordCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<ChangePasswordCommandDTO> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.ChangePassword(request);
            return response;
        }
    }
}
