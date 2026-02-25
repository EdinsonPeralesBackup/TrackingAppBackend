namespace Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact
{
    public class ChangeStatusConfidenceContactCommandDTO
    {
        public string Message { get; set; }
        public int ContactId { get; set; }
        public bool Status { get; set; }
    }
}
