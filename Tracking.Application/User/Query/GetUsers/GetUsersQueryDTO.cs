namespace Tracking.Application.User.Query.GetUsers
{
    public class GetUsersQueryDTO
    {
        public GetUserData[] Users { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
