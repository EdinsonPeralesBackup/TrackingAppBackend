using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;

namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, SendVerificationCodeCommandDTO>
    {
        private readonly ILogger<SendVerificationCodeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITwilioService _twilioService;

        public SendVerificationCodeCommandHandler(
            ILogger<SendVerificationCodeCommandHandler> logger,
            IMapper mapper,
            ITwilioService twilioService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._twilioService = twilioService;
        }
        public Task<SendVerificationCodeCommandDTO> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var sid = this._twilioService.SendVerificationCode(request.Phone);
            var response = new SendVerificationCodeCommandDTO()
            {
                Message = sid == "pending" ? $"Recovery code sent to +51 {request.Phone}" : "Error, code not sent",
                ExpiresIn = sid == "pending" ? 360 : 0,
            };
            return Task.FromResult(response);
        }
    }
}
