namespace Tracking.Application.TrustedContacts.Query.GetTrustedContact
{
    public class GetTrustedContactQueryDTO
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}
