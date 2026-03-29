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
        public async Task<IEnumerable<GetTrackingHistoryQueryDTO>> Handle(GetTrackingHistoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this._mapsRepository.GetTrackingHistory(request);
            if (request.EsRutaActual)
            {
                var first = response.FirstOrDefault();
                if (first != null)
                {
                    var coordinates = await _mapsRepository.GetPointOfRoute(first.IdRoute);
                    first.Coordinates = [.. coordinates];
                }
            }
            return response;
        }
    }
}
