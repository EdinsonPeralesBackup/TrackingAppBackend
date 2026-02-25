using MediatR;

namespace Tracking.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryDTO>
    {
        public int Id { get; set; }
    }
}
