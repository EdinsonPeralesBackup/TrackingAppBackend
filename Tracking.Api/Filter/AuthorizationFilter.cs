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

                    var userId = claims.FirstOrDefault(c => c.Type == "identifier")?.Value;
                    var userName = claims.FirstOrDefault(c => c.Type == "nombres")?.Value;
                    var userApellidoPaterno = claims.FirstOrDefault(c => c.Type == "apellido_paterno")?.Value;
                    var userApellidoMaterno = claims.FirstOrDefault(c => c.Type == "apellido_materno")?.Value;
                    var userFullName = claims.FirstOrDefault(c => c.Type == "nombre_completo")?.Value;
                    var rolId = claims.FirstOrDefault(c => c.Type == "id_rol")?.Value;
                    var rolName = claims.FirstOrDefault(c => c.Type == "rol_nombre")?.Value;

                    var currentUser = new CurrentUser()
                    {
                        Identifier = userId,
                        Nombres = userName,
                        ApellidoPaterno = userApellidoPaterno,
                        ApellidoMaterno = userApellidoMaterno,
                        NombreCompleto = userFullName,
                        RolId = rolId,
                        Rol = rolName
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
