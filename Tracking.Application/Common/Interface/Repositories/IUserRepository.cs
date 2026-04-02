using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.ValidToken;
using Tracking.Application.User.Comand.ChangePassword;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.User.Comand.UpdateUserInfo;
using Tracking.Application.User.Query.GetUserById;
using Tracking.Application.User.Query.GetUsers;
using Tracking.Application.VerificationCode.Command.ResetPassword;
using Tracking.Application.VerificationCode.Command.SendVerificationCode;

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
        Task<UpdateUserInfoCommandDTO> UpdateUserInfo(UpdateUserInfoCommand command);
        Task<GetUsersQueryDTO> GetUser(GetUsersQuery command);
        Task<GetUserByIdQueryDTO> GetUserById(GetUserByIdQuery command);
        Task<ChangePasswordCommandDTO> ChangePassword(ChangePasswordCommand command);
    }
}
