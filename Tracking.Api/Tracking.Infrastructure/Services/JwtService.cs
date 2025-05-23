using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Settings;

namespace Tracking.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public JwtService(IOptions<JwtSettings> jwtSettings, IDateTimeService dateTimeService)
        {
            this._jwtSettings = jwtSettings.Value;
            this._dateTimeService = dateTimeService;
        }

        public string Generate(Claim[] claims, DateTime? experisUtc = null, string audience = null)
        {
            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: audience,
                        claims: claims,
                        expires: _dateTimeService.HoraLocal().AddSeconds(_jwtSettings.ExpiresInSeconds),
                        signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
