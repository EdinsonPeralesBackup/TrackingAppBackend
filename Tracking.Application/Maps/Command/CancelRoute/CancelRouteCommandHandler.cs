using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Command.CancelRoute
{
    public class CancelRouteCommandHandler : IRequestHandler<CancelRouteCommand, CancelRouteCommandDTO>
    {
        private readonly ILogger<CancelRouteCommandHandler> _logger;
        private readonly IMapsRepository _mapsRepository;

        public CancelRouteCommandHandler(
            ILogger<CancelRouteCommandHandler> logger,
            IMapsRepository mapsRepository)
        {
            this._logger = logger;
            this._mapsRepository = mapsRepository;
        }
        public Task<CancelRouteCommandDTO> Handle(CancelRouteCommand request, CancellationToken cancellationToken)
        {
            var response = this._mapsRepository.CancelRoute(request);
            return response;
        }
    }
}
