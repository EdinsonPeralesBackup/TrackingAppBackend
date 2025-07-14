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

namespace Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact
{
    public class ChangeStatusConfidenceContactCommandHandler : IRequestHandler<ChangeStatusConfidenceContactCommand, ChangeStatusConfidenceContactCommandDTO>
    {
        private readonly ILogger<ChangeStatusConfidenceContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public ChangeStatusConfidenceContactCommandHandler(
            ILogger<ChangeStatusConfidenceContactCommandHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<ChangeStatusConfidenceContactCommandDTO> Handle(ChangeStatusConfidenceContactCommand request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.ChangeStatusTrustedContacts(request);
            return response;
        }
    }
}
