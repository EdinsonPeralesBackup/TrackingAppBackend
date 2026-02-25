namespace Tracking.Application.VerificationCode.Command.SendVerificationCode
{
    public class InsertCodeResetCommand
    {
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}