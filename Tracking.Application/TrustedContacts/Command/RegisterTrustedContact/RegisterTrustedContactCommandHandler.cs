using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.TrustedContacts.Command.RegisterTrustedContact
{
    public class RegisterTrustedContactCommandHandler : IRequestHandler<RegisterTrustedContactCommand, RegisterTrustedContactCommandDTO>
    {
        private readonly ILogger<RegisterTrustedContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public RegisterTrustedContactCommandHandler(
            ILogger<RegisterTrustedContactCommandHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<RegisterTrustedContactCommandDTO> Handle(RegisterTrustedContactCommand request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.RegisterTrustedContacts(request);
            return response;
        }
    }
}
