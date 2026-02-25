using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Authorization.Commad.Register;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface IAuthorizationRepository
    {
        Task<LoginCommandDTO> Login(LoginCommand command);
        Task<RegisterCommandDTO> Register(RegisterCommand command);
    }
}
