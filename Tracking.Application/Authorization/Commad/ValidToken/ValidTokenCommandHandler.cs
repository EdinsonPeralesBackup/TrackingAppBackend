using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Authorization.Commad.ValidToken
{
    public class ValidTokenCommandHandler : IRequestHandler<ValidTokenCommand, ValidTokenCommandDTO>
    {
        private readonly ILogger<ValidTokenCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ValidTokenCommandHandler(
            ILogger<ValidTokenCommandHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<ValidTokenCommandDTO> Handle(ValidTokenCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
