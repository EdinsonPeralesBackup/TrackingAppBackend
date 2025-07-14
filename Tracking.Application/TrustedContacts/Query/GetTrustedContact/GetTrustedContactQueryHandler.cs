using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.TrustedContacts.Command.RegisterTrustedContact;

namespace Tracking.Application.TrustedContacts.Query.GetTrustedContact
{
    public class GetTrustedContactQueryHandler : IRequestHandler<GetTrustedContactQuery, IEnumerable<GetTrustedContactQueryDTO>>
    {
        private readonly ILogger<GetTrustedContactQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public GetTrustedContactQueryHandler(
            ILogger<GetTrustedContactQueryHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<IEnumerable<GetTrustedContactQueryDTO>> Handle(GetTrustedContactQuery request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.GetTrustedContacts(request);
            return response;
        }
    }
}
