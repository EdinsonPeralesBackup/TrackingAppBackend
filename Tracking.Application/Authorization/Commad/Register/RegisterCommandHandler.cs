using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Authorization.Commad.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandDTO>
    {
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorizationRepository _authorizationRepository;

        public RegisterCommandHandler(
            ILogger<RegisterCommandHandler> logger,
            IMapper mapper,
            IAuthorizationRepository authorizationRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._authorizationRepository = authorizationRepository;
        }
        public Task<RegisterCommandDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = this._authorizationRepository.Register(request);
            return response;
        }
    }
}
