using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Maps.Command.UpdatePoint
{
    public class UpdatePointCommandHandler : IRequestHandler<UpdatePointCommand, UpdatePointCommandDTO>
    {
        private readonly ILogger<UpdatePointCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;

        public UpdatePointCommandHandler(
            ILogger<UpdatePointCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
        }
        public Task<UpdatePointCommandDTO> Handle(UpdatePointCommand request, CancellationToken cancellationToken)
        {
            var response = this._mapsRepository.UpdatePoint(request);
            return response;
        }
    }
}
