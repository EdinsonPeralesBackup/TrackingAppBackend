using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
