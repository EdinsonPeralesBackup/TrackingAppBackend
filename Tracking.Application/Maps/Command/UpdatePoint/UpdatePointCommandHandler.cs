using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;
using Microsoft.AspNetCore.SignalR;
using Tracking.Application.Common.Hubs;
using Tracking.Application.Maps.Query.GetDangerRoute;
using Tracking.Application.Common.Interface;

namespace Tracking.Application.Maps.Command.UpdatePoint
{
    public class UpdatePointCommandHandler : IRequestHandler<UpdatePointCommand, UpdatePointCommandDTO>
    {
        private readonly ILogger<UpdatePointCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;
        private readonly IHubContext<DangerHub> _hub;
        private readonly IDateTimeService _dateTimeService;

        public UpdatePointCommandHandler(
            ILogger<UpdatePointCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            IHubContext<DangerHub> hub,
            IDateTimeService dateTimeService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
            this._hub = hub;
            this._dateTimeService = dateTimeService;
        }
        public async Task<UpdatePointCommandDTO> Handle(UpdatePointCommand request, CancellationToken cancellationToken)
        {
            var response = await this._mapsRepository.UpdatePoint(request);
            if (response.Danger != null)
            {
                await _hub.Clients.Group(response.Danger)
                    .SendAsync("ReceiveLocation", new GetDangerRouteQueryDTO
                    {
                        Latitude = request.Coordinates.Latitude,
                        Longitude = request.Coordinates.Longitude,
                        Timestamp = this._dateTimeService.HoraLocal()
                    });
            }
            return response;
        }
    }
}
