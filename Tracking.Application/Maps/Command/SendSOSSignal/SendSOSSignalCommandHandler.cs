using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ITrustedContactRepository _trustedContactRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITwilioService _twilioService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAcortadorServices _acortadorServices;

        public SendSOSSignalCommandHandler(
            ILogger<SendSOSSignalCommandHandler> logger,
            IMapper mapper,
            IMapsRepository mapsRepository,
            ITrustedContactRepository trustedContactRepository,
            IUserRepository userRepository,
            ITwilioService twilioService,
            IDateTimeService dateTimeService,
            IAcortadorServices acortadorServices)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._trustedContactRepository = trustedContactRepository;
            this._userRepository = userRepository;
            this._twilioService = twilioService;
            this._dateTimeService = dateTimeService;
            this._acortadorServices = acortadorServices;
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

            var alert = await this._trustedContactRepository.RegisterAlert(
                                    new RegisterAlert()
                                    {
                                        IdUser = request.UserId,
                                        TrackingId = request.TrackingId,
                                        Coordinate = request.Coordinate,
                                        DateRegister = this._dateTimeService.HoraLocal()
                                    });

            if (alert.Equals("EX"))
            {
                return new SendSOSSignalCommandDTO()
                {
                    Message = "Error in sent SMS.",
                    FallBackUsed = false
                };
            }

            foreach (var contact in contactTrusted)
            {
                string RutaBase = "https://localhost:7128";
                string Endpoint = "/api/v1/Code/registerVisit/";
                string RutaFinal = $"{RutaBase}{Endpoint}{request.TrackingId}";
                string rutaAcortada = await this._acortadorServices.AcordarEnlace(RutaFinal);

                var response = this._twilioService.SendSOS(
                    contact.Phone,
                    user,
                    request.Coordinate,
                    rutaAcortada);
            }

            return new SendSOSSignalCommandDTO()
            {
                Message = "SOS sent to contacts.",
                FallBackUsed = request.Coordinate == null
            };
        }
    }
}
