using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Query.GetDangerRoute
{
    public class GetDangerRouteQueryHandler : IRequestHandler<GetDangerRouteQuery, IEnumerable<GetDangerRouteQueryDTO>>
    {
        private readonly ILogger<GetDangerRouteQueryHandler> _logger;
        private readonly IMapsRepository _mapsRepository;

        public GetDangerRouteQueryHandler(
            ILogger<GetDangerRouteQueryHandler> logger,
            IMapsRepository mapsRepository)
        {
            this._logger = logger;
            this._mapsRepository = mapsRepository;
        }
        public Task<IEnumerable<GetDangerRouteQueryDTO>> Handle(GetDangerRouteQuery request, CancellationToken cancellationToken)
        {
            var response = this._mapsRepository.GetDangerRoute(request);
            return response;
        }
    }
}
