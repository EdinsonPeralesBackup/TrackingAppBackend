using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;

namespace Tracking.Application.Maps.Command.ArriveRoute
{
    public class ArriveRouteCommandHandler : IRequestHandler<ArriveRouteCommand, ArriveRouteCommandDTO>
    {
        private readonly ILogger<ArriveRouteCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public ArriveRouteCommandHandler(
            ILogger<ArriveRouteCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
            this._trustedContactRepository = trustedContactRepository;
        }
        public async Task<ArriveRouteCommandDTO> Handle(ArriveRouteCommand request, CancellationToken cancellationToken)
        {
            var contactUser = await this._trustedContactRepository.GetTrustedContacts(new GetTrustedContactQuery()
            {
                IdUser = request.UserId
            });
            var response = await this._mapsRepository.ArriveRoute(request);
            response.ContactsNotified = contactUser.ToList();
            return response;
        }
    }
}
