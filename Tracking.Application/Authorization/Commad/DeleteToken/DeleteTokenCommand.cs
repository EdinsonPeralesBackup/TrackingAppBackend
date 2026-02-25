using MediatR;

namespace Tracking.Application.Authorization.Commad.DeleteToken
{
    public class DeleteTokenCommand : IRequest<DeleteTokenCommandDTO>
    {
        public int IdUser { get; set; }
    }
}
