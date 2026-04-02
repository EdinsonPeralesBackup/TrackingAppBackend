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
        //string messagingServicesId;

        public TwilioService(
            IAcortadorServices acortadorServices)
        {
            this.accountSid = "ACb617e3ccb5c2b18d8f8681b4501f03f9";
            this.authToken = "ca8171e762f0d70c491627fa49baa9e3";
            this.numberPhone = "+18503184909";
            //this.messagingServicesId = "VA8c2e65202f923c86252eb99b0b3b6494";
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
