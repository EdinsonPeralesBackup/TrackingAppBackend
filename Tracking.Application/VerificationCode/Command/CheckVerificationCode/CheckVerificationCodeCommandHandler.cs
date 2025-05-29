using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.VerificationCode.Command.SendVerificationCode;

namespace Tracking.Application.VerificationCode.Command.CheckVerificationCode
{
    public class CheckVerificationCodeCommandHandler : IRequestHandler<CheckVerificationCodeCommand, CheckVerificationCodeCommandDTO>
    {
        private readonly ILogger<SendVerificationCodeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITwilioService _twilioService;
        private readonly IUserRepository _userRepository;

        public CheckVerificationCodeCommandHandler(
            ILogger<SendVerificationCodeCommandHandler> logger,
            IMapper mapper,
            ITwilioService twilioService,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._twilioService = twilioService;
            this._userRepository = userRepository;
        }
        public async Task<CheckVerificationCodeCommandDTO> Handle(CheckVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var status = this._twilioService.CheckVerificationCode(request.PhoneNumber, request.VerificationCode);
            var response = new CheckVerificationCodeCommandDTO();
            if (status != "approved")
            {
                return response;
            }
            var random = new Random();
            var resetCode = random.Next(1, 1000000).ToString("D6");
            var insert = await this._userRepository.InsertCodeReset(new InsertCodeResetCommand()
            {
                Code = resetCode,
                IdUser = request.IdUser
            });
            if (insert.Message != "")
            {
                response = new CheckVerificationCodeCommandDTO()
                {
                    Message = insert.Message != "" ? $"Code verified successfully" : "Code verification failed",
                    VerificationCode = insert.Message != "" ? resetCode : ""
                };
            }
            return response;
        }
    }
}
