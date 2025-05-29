using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.ValidToken;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.VerificationCode.Command.CheckVerificationCode;
using Tracking.Application.VerificationCode.Command.ResetPassword;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface IUserRepository
    {
        Task<DeleteUserCommandDTO> DeleteUser(DeleteUserCommand command);
        Task<InsertCodeResetCommandDTO> InsertCodeReset(InsertCodeResetCommand command);
        Task<ResetPasswordCommandDTO> ResetPassword(ResetPasswordCommand command);
        void InsertToken(string token, int idUser);
        void DeleteToken(DeleteTokenCommand command);
        Task<ValidTokenCommandDTO> ValidToken(ValidTokenCommand command);
    }
}
