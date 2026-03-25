using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Command.DangerRoute
{
    public class DangerRouteCommandHandler : IRequestHandler<DangerRouteCommand, DangerRouteCommandDTO>
    {
        private readonly ILogger<DangerRouteCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;
        private readonly IDateTimeService _dateTimeService;

        public DangerRouteCommandHandler(
            ILogger<DangerRouteCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            IDateTimeService dateTimeService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
            this._dateTimeService = dateTimeService;
        }
        public Task<DangerRouteCommandDTO> Handle(DangerRouteCommand request, CancellationToken cancellationToken)
        {
            request.DateDanger = this._dateTimeService.HoraLocal();
            var response = this._mapsRepository.DangerRoute(request);
            return response;
        }
    }
}
