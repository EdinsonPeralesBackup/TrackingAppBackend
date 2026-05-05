using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Command.FinishDangerRoute
{
    public class FinishDangerRouteCommandHandler : IRequestHandler<FinishDangerRouteCommand, FinishDangerRouteCommandDTO>
    {
        private readonly ILogger<FinishDangerRouteCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;

        public FinishDangerRouteCommandHandler(
            
            ILogger<FinishDangerRouteCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
        }
        public Task<FinishDangerRouteCommandDTO> Handle(FinishDangerRouteCommand request, CancellationToken cancellationToken)
        {
            var response = this._mapsRepository.FinishDangerRoute(request);
            return response;
        }
    }
}
