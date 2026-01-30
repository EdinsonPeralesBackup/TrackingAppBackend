using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.User.Query.GetUserById;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Tracking.Infrastructure.Services
{
    public class TwilioService : ITwilioService
    {
        string accountSid;
        string authToken;
        string pathServiceSid;
        string numberPhone;
        string messagingServicesId;
        public TwilioService()
        {
            this.accountSid = "ACb617e3ccb5c2b18d8f8681b4501f03f9";
            this.authToken = "b863421c2d63e827ffcb8caeae1943e7";
            this.pathServiceSid = "VA8c2e65202f923c86252eb99b0b3b6494";
            this.numberPhone = "+14155238886";
            this.messagingServicesId = "MGc6ce529a20d4c2d97c049282cda9d1f7";
        }
        public string SendVerificationCode(string phone, string message)
        {
            TwilioClient.Init(this.accountSid, this.authToken);
            var messageOptions = new CreateMessageOptions(new PhoneNumber("+51" + phone));
            messageOptions.MessagingServiceSid = this.messagingServicesId;
            messageOptions.Body = message;
            var send = MessageResource.Create(messageOptions);

            return send.Status.ToString();
        }

        public string SendSOS(string phone, GetUserByIdQueryDTO getUserById, Coordinates? coordinates)
        {
            var messageBody = new StringBuilder();
            messageBody.AppendLine($"Su contacto de confianza {getUserById.Name} {getUserById.LastName}, ha enviado un mensaje de emergencia.");
            if (coordinates != null)
            {
                messageBody.AppendLine($"Coordenadas: {coordinates.Latitude}, {coordinates.Longitude}");
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
