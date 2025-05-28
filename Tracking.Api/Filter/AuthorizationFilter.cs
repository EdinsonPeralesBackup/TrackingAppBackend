using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tracking.Application.Common.Interface;
using Tracking.Api.Services;

namespace Tracking.Api.Filter
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string _key = "WCM9K1M2&7g1O4bogUii$TYxWwTP@S*1";
            string _issuer = "Tracking.Api";

            var request = context.HttpContext.Request;

            if (!request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var handler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
                };

                var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var claims = jwtToken.Claims;

                    var userId = claims.FirstOrDefault(c => c.Type == "id")?.Value;
                    var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    var lastName = claims.FirstOrDefault(c => c.Type == "lastName")?.Value;
                    var birthday = claims.FirstOrDefault(c => c.Type == "birthday")?.Value;
                    var phone = claims.FirstOrDefault(c => c.Type == "phone")?.Value;
                    var fullName = claims.FirstOrDefault(c => c.Type == "fullName")?.Value;
                    var idRol = claims.FirstOrDefault(c => c.Type == "idRol")?.Value;
                    var rolName = claims.FirstOrDefault(c => c.Type == "rolNombre")?.Value;

                    var currentUser = new CurrentUser()
                    {
                        Id = userId,
                        Name = name,
                        LastName = lastName,
                        Birthday = birthday,
                        Phone = phone,
                        FullName = fullName,
                        IdRol = Convert.ToInt32(idRol),
                        RolNombre = rolName
                    };

                    var currenUserSerialize = JsonConvert.SerializeObject(currentUser);

                    context.HttpContext.Session.SetString("dataUser", currenUserSerialize);
                }

                Debug.WriteLine("Token válido.");
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
