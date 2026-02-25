namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class SendVerificationCodeCommandDTO
    {
        public string Message { get; set; }
        public int ExpiresIn { get; set; }
    }
}
