namespace Tracking.Application.TrustedContacts.Command.UpdateTrustedContact
{
    public class UpdateTrustedContactCommandDTO
    {
        public string Message { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}
