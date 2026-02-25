using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Query.GetTrackingHistory
{
    public class GetTrackingHistoryQueryHandler : IRequestHandler<GetTrackingHistoryQuery, IEnumerable<GetTrackingHistoryQueryDTO>>
    {
        private readonly ILogger<GetTrackingHistoryQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;

        public GetTrackingHistoryQueryHandler(
            ILogger<GetTrackingHistoryQueryHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
        }
        public Task<IEnumerable<GetTrackingHistoryQueryDTO>> Handle(GetTrackingHistoryQuery request, CancellationToken cancellationToken)
        {
            var response = this._mapsRepository.GetTrustedContacts(request);
            return response;
        }
    }
}
