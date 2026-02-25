namespace Tracking.Application.User.Query.GetUsers
{
    public class GetUserData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
