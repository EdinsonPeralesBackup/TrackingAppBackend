namespace Tracking.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
