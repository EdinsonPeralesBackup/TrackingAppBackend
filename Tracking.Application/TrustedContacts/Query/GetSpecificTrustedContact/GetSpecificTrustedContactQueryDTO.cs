namespace Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact
{
    public class GetSpecificTrustedContactQueryDTO
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}
