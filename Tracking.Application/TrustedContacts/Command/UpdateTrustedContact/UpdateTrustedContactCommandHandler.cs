using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.TrustedContacts.Command.UpdateTrustedContact
{
    public class UpdateTrustedContactCommandHandler : IRequestHandler<UpdateTrustedContactCommand, UpdateTrustedContactCommandDTO>
    {
        private readonly ILogger<UpdateTrustedContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public UpdateTrustedContactCommandHandler(
            ILogger<UpdateTrustedContactCommandHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<UpdateTrustedContactCommandDTO> Handle(UpdateTrustedContactCommand request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.UpdateTrustedContacts(request);
            return response;
        }
    }
}
