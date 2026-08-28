using Microsoft.Extensions.Options;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Settings;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Tracking.Infrastructure.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly TwilioSettings _twilioSettings;

        public TwilioService(IOptions<TwilioSettings> twilioOptions)
        {
            _twilioSettings = twilioOptions.Value;

            TwilioClient.Init(
                _twilioSettings.AccountSid,
                _twilioSettings.AuthToken
            );
        }

        public async Task<string> SendVerificationCodeAsync(string phone)
        {
            var verification = await VerificationResource.CreateAsync(
                to: "+51" + phone,
                channel: "sms",
                pathServiceSid: _twilioSettings.VerifyServiceSid
            );

            return verification.Status;
        }

        public async Task<bool> CheckVerificationCodeAsync(
            string phone,
            string code)
        {
            var verificationCheck =
                await VerificationCheckResource.CreateAsync(
                    to: "+51" + phone,
                    code: code,
                    pathServiceSid: _twilioSettings.VerifyServiceSid
                );

            return verificationCheck.Status == "approved";
        }
    }
}