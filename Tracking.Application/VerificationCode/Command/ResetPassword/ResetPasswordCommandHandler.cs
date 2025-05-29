using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.VerificationCode.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordCommandDTO>
    {
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(
            ILogger<ResetPasswordCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<ResetPasswordCommandDTO> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.ResetPassword(request);
            return response;
        }
    }
}
