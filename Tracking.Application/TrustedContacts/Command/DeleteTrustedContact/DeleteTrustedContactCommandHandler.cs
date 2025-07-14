using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.TrustedContacts.Command.DeleteTrustedContact
{
    public class DeleteTrustedContactCommandHandler : IRequestHandler<DeleteTrustedContactCommand, DeleteTrustedContactCommandDTO>
    {
        private readonly ILogger<DeleteTrustedContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITrustedContactRepository _trustedContactRepository;

        public DeleteTrustedContactCommandHandler(
            ILogger<DeleteTrustedContactCommandHandler> logger,
            IMapper mapper,
            ITrustedContactRepository trustedContactRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
        }
        public Task<DeleteTrustedContactCommandDTO> Handle(DeleteTrustedContactCommand request, CancellationToken cancellationToken)
        {
            var response = this._trustedContactRepository.DeleteTrustedContacts(request);
            return response;
        }
    }
}
