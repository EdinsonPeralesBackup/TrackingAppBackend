using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact
{
    public class GetSpecificTrustedContactQueryHandler : IRequestHandler<GetSpecificTrustedContactQuery, GetSpecificTrustedContactQueryDTO>
    {
        private readonly ILogger<GetSpecificTrustedContactQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public GetSpecificTrustedContactQueryHandler(
            ILogger<GetSpecificTrustedContactQueryHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<GetSpecificTrustedContactQueryDTO> Handle(GetSpecificTrustedContactQuery request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.GetSpecificTrustedContacts(request);
            return response;
        }
    }
}
