using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Tracking.Infrastructure.Services
{
    public class TwilioService : ITwilioService
    {
        string accountSid;
        string authToken;
        string pathServiceSid;
        public TwilioService()
        {
            this.accountSid = "ACb617e3ccb5c2b18d8f8681b4501f03f9";
            this.authToken = "150827263387da679a044c6b445331d3";
            this.pathServiceSid = "VA8c2e65202f923c86252eb99b0b3b6494";
        }
        public string SendVerificationCode(string phone)
        {
            TwilioClient.Init(this.accountSid, this.authToken);

            var verification = VerificationResource.Create(
                to: "+51" + phone,
                channel: "sms",
                pathServiceSid: pathServiceSid
            );

            return verification.Status;
        }

        public string CheckVerificationCode(string phone, string code)
        {
            TwilioClient.Init(this.accountSid, this.authToken);
            var verificationCheck = VerificationCheckResource.Create(
                to: "+51" + phone,
                code: code,
                pathServiceSid: pathServiceSid
            );
            return verificationCheck.Status;
        }
    }
}
