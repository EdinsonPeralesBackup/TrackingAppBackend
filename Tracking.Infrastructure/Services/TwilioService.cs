using Microsoft.Extensions.Configuration;
using System.Text;
using Tracking.Application.Common.Interface;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.User.Query.GetUserById;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Tracking.Infrastructure.Services
{
    public class TwilioService : ITwilioService
    {
        string accountSid;
        string authToken;
        string numberPhone;

        public TwilioService(
            IAcortadorServices acortadorServices,
            IConfiguration configuration )
        {
            this.accountSid = configuration["twilio:accountSid"] ?? "";
            this.authToken = configuration["twilio:authToken"] ?? "";
            this.numberPhone = configuration["twilio:fromPhoneNumber"] ?? "";
        }
        public string SendVerificationCode(string phone, string message)
        {
            TwilioClient.Init(this.accountSid, this.authToken);
            var messaget = MessageResource.Create(
                body: message.ToString(),
                from: this.numberPhone,
                to: "+51" + phone
            );
            return messaget.Status.ToString();
        }

        public string SendSOS(string phone, GetUserByIdQueryDTO getUserById, Coordinates? coordinates, string rutaAcortada)
        {
            var messageBody = new StringBuilder();

            messageBody.AppendLine($"Su contacto de confianza {getUserById.Name} {getUserById.LastName}, ha enviado un mensaje de emergencia.");
            if (coordinates != null)
            {
                messageBody.AppendLine($"Coordenadas: {coordinates.Latitude}, {coordinates.Longitude}");
                messageBody.AppendLine($"Puede visitar el viaje en el siguiente enlace: {rutaAcortada}");
            }
            else
            {
                messageBody.AppendLine("No se proporcionaron coordenadas.");
            }

            TwilioClient.Init(this.accountSid, this.authToken);
            var message = MessageResource.Create(
                body: messageBody.ToString(),
                from: this.numberPhone,
                to: "+51" + phone
            );
            return message.Sid;
        }
    }
}
