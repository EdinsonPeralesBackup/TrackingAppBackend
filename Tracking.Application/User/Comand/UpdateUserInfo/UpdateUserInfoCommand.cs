using MediatR;

namespace Tracking.Application.User.Comand.UpdateUserInfo
{
    public class UpdateUserInfoCommand : IRequest<UpdateUserInfoCommandDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phonenumber { get; set; }
        public string Avatar { get; set; }
    }
}
