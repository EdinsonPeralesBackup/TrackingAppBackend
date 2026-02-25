using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.TrustedContacts.Command.RegisterVisit
{
    public class RegisterVisitCommandHandler : IRequestHandler<RegisterVisitCommand, RegisterVisitCommandDTO>
    {
        private readonly ILogger<RegisterVisitCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public RegisterVisitCommandHandler(
            ILogger<RegisterVisitCommandHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<RegisterVisitCommandDTO> Handle(RegisterVisitCommand request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.RegisterVisit(request);
            return response;
        }
    }
}
