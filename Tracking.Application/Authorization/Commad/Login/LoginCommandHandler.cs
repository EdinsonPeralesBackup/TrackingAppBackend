using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.Authorization.Commad.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandToken>
    {
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IDateTimeService _dateTimeService;

        public LoginCommandHandler(
            ILogger<LoginCommandHandler> logger,
            IMapper mapper,
            IAuthorizationRepository authorizationRepository,
            IUserRepository userRepository,
            IJwtService jwtService,
            IDateTimeService dateTimeService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._authorizationRepository = authorizationRepository;
            this._userRepository = userRepository;
            this._jwtService = jwtService;
            this._dateTimeService = dateTimeService;
        }
        public async Task<LoginCommandToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = new LoginCommandToken();
            var response = await _authorizationRepository.Login(request);
            if (response.Id == 0)
            {
                return token;
            }
            token.Token = GenerateToken(response);
            await this._userRepository.InsertToken(token.Token, response.Id);
            return token;
        }

        private string GenerateToken(LoginCommandDTO command)
        {
            var claims = new List<Claim>
            {
                new Claim("id", command.Id.ToString() ?? ""),
                new Claim("name", command.Name ?? ""),
                new Claim("lastName", command.Lastname ?? ""),
                new Claim("birthday", command.Birthday ?? ""),
                new Claim("phone", command.Phone ?? ""),
                new Claim("fullName", $"{command.Name} {command.Lastname}"),
                new Claim("idRol", command.IdRole.ToString() ?? ""),
                new Claim("rolNombre", command.Role ?? ""),
            };

            var token = _jwtService.Generate(claims.ToArray(), this._dateTimeService.HoraLocal());

            return token;
        }
    }
}
