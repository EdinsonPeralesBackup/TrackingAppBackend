using MediatR;

namespace Tracking.Application.User.Comand.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserCommandDTO>
    {
        public int IdUser { get; set; }
    }
}
