using MediatR;

namespace Tracking.Application.Authorization.Commad.ValidToken
{
    public class ValidTokenCommand : IRequest<ValidTokenCommandDTO>
    {
        public string Token { get; set; }
    }
}
