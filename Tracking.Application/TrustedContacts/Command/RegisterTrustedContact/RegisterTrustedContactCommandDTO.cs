namespace Tracking.Application.TrustedContacts.Command.RegisterTrustedContact
{
    public class RegisterTrustedContactCommandDTO
    {
        public string Message { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}
