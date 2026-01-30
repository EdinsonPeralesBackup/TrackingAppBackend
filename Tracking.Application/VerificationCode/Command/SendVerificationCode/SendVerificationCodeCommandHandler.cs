using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, SendVerificationCodeCommandDTO>
    {
        private readonly ILogger<SendVerificationCodeCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITwilioService _twilioService;

        public SendVerificationCodeCommandHandler(
            ILogger<SendVerificationCodeCommandHandler> logger,
            IUserRepository userRepository,
            IMapper mapper,
            ITwilioService twilioService)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._twilioService = twilioService;
        }
        public async Task<SendVerificationCodeCommandDTO> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var codeReset = new Random().Next(0, 1000000).ToString("D6");
            var message = "Tracking APP Codigo de verificacion: " + codeReset + "\nRecuerda no compartir el codigo con nadie.";
            var sid = "";
            InsertCodeResetCommandDTO Insert = await this._userRepository.InsertCodeReset(new InsertCodeResetCommand()
            {
                Code = codeReset
            });
            if (Insert.Message == "Code registed successfully.")
            {
                sid = this._twilioService.SendVerificationCode(request.Phone, message);
            }

            var response = new SendVerificationCodeCommandDTO()
            {
                Message = sid == "accepted" ? $"Recovery code sent to +51 {request.Phone}" : "Error, code not sent",
                ExpiresIn = sid == "accepted" ? 360 : 0,
            };
            return response;
        }
    }
}
