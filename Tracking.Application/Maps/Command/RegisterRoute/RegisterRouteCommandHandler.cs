using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Command.ObtenerRuta
{
    public class RegisterRouteCommandHandler : IRequestHandler<RegisterRouteCommand, RegisterRouteCommandDTO>
    {
        private readonly ILogger<RegisterRouteCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;
        private readonly IMapsServices _mapsServices;

        public RegisterRouteCommandHandler(
            ILogger<RegisterRouteCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            IMapsServices mapsServices
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
            this._mapsServices = mapsServices;
        }

        public async Task<RegisterRouteCommandDTO> Handle(RegisterRouteCommand request, CancellationToken cancellationToken)
        {
            var respuestaMaps = await _mapsServices.ObtenerRuta(request);
            var routes= JObject.Parse(respuestaMaps)["routes"][0];
            var legs = routes["legs"];
            var steps = new List<Route>();
            if (legs != null)
            {
                foreach (var leg in legs)
                {
                    var dto = new Route
                    {
                        Distance = leg["distance"]?.ToObject<Distance>(),
                        Duration = leg["duration"]?.ToObject<Duration>(),
                        StartAddress = leg["start_address"]?.ToString(),
                        EndAddress = leg["end_address"]?.ToString(),
                        StartLocation = leg["start_location"]?.ToObject<Location>(),
                        EndLocation = leg["end_location"]?.ToObject<Location>(),
                        Steps = leg["steps"]?.ToObject<List<Step>>()
                    };

                    steps.Add(dto);
                }
            }

            var response = await this._mapsRepository.RegisterRoute(steps.FirstOrDefault(), request.UserId);
            if (response.TrackingId == "EX")
            {
                response.RouteTravel = null;
            }

            return response;
        }
    }
}
