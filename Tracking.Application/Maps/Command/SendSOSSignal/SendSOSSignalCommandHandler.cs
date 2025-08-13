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
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;
using Tracking.Application.User.Query.GetUserById;

namespace Tracking.Application.Maps.Command.SendSOSSignal
{
    public class SendSOSSignalCommandHandler : IRequestHandler<SendSOSSignalCommand, SendSOSSignalCommandDTO>
    {
        private readonly ILogger<SendSOSSignalCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMapsRepository _mapsRepository;
        private readonly ITrustedContactRepository _trustedContactRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITwilioService _twilioService;

        public SendSOSSignalCommandHandler(
            ILogger<SendSOSSignalCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            ITrustedContactRepository trustedContactRepository,
            IUserRepository userRepository,
            ITwilioService twilioService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mapsRepository = mapsRepository;
            this._trustedContactRepository = trustedContactRepository;
            this._userRepository = userRepository;
            this._twilioService = twilioService;
        }
        public async Task<SendSOSSignalCommandDTO> Handle(SendSOSSignalCommand request, CancellationToken cancellationToken)
        {
            var user = (await this._userRepository.GetUserById(
                                new GetUserByIdQuery()
                                {
                                    Id = request.UserId
                                }));
            var contactTrusted = (await this._trustedContactRepository.GetTrustedContacts(
                                    new GetTrustedContactQuery()
                                    {
                                        IdUser = request.UserId
                                    })).ToList();
            foreach (var contact in contactTrusted)
            {
                var response = this._twilioService.SendSOS(
                    contact.Phone,
                    user,
                    request.Coordinate);
            }
            return new SendSOSSignalCommandDTO()
            {
                Message = "SOS sent to contacts.",
                FallBackUsed = request.Coordinate == null
            };
        }
    }
}
