using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Authorization.Commad.ValidToken;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.VerificationCode.Command.CheckVerificationCode;

using Tracking.Application.VerificationCode.Command.ResetPassword;
using Tracking.Persistence.Database;

namespace Tracking.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBase _dataBase;
        private readonly ICryptography _cryptography;

        public UserRepository(IServiceProvider serviceProvider, ICryptography cryptography)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
            this._cryptography = cryptography;
        }

        public async Task<DeleteUserCommandDTO> DeleteUser(DeleteUserCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pIdUser", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@msj", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[usp_DeleteUser]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("msj");
                return new DeleteUserCommandDTO()
                {
                    Message = mensaje
                };
            }
        }

        public async Task<InsertCodeResetCommandDTO> InsertCodeReset(InsertCodeResetCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pcode", command.Code, DbType.String, ParameterDirection.Input);
                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterCodeReset]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("message");
                return new InsertCodeResetCommandDTO()
                {
                    Message = mensaje
                };
            }
        }

        public async Task<ResetPasswordCommandDTO> ResetPassword(ResetPasswordCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@presetCode", command.Code, DbType.String, ParameterDirection.Input);
                parameters.Add("@pnewPassword", this._cryptography.Encrypt(command.NewPassword), DbType.String, ParameterDirection.Input);
                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_ResetPassword]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("message");
                return new ResetPasswordCommandDTO()
                {
                    Message = mensaje
                };
            }
        }

        public async void InsertToken(string token, int idUser)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pToken", token, DbType.String, ParameterDirection.Input);
                parameters.Add("@pidUser", idUser, DbType.Int32, ParameterDirection.Input);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterToken]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async void DeleteToken(DeleteTokenCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_DeleteToken]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ValidTokenCommandDTO> ValidToken(ValidTokenCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@ptoken", command.Token, DbType.String, ParameterDirection.Input);
                parameters.Add("@count", false, DbType.Boolean, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_ValidToken]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var count = parameters.Get<bool>("count");
                return new ValidTokenCommandDTO()
                {
                    IsValid = count
                };
            }
        }
    }
}
