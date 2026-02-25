using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Tracking.Application.Authorization.Commad.DeleteToken;
using Tracking.Application.Authorization.Commad.ValidToken;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.User.Comand.DeleteUser;
using Tracking.Application.User.Comand.UpdateUserInfo;
using Tracking.Application.User.Query.GetUserById;
using Tracking.Application.User.Query.GetUsers;
using Tracking.Application.VerificationCode.Command.ResetPassword;
using Tracking.Application.VerificationCode.Command.SendVerificationCode;
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
                parameters.Add("@pphone", command.Phone, DbType.String, ParameterDirection.Input);
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

        public async Task<UpdateUserInfoCommandDTO> UpdateUserInfo(UpdateUserInfoCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pId", command.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pname", command.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@plastName", command.Lastname, DbType.String, ParameterDirection.Input);
                parameters.Add("@pbirthday", command.Birthday, DbType.Date, ParameterDirection.Input);
                parameters.Add("@pphoneNumber", command.Phonenumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@pavatarImg", command.Avatar, DbType.String, ParameterDirection.Input);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[usp_EditUserInfo]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("message");
                return new UpdateUserInfoCommandDTO()
                {
                    Message = mensaje
                };
            }
        }

        public async Task<GetUsersQueryDTO> GetUser(GetUsersQuery command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@ppage", command.Page, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@plimit", command.Limit, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@total", 0, DbType.Int32, ParameterDirection.Output);

                List<GetUserData> response = new List<GetUserData>();

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetUsers]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    while (reader.Read())
                    {
                        response.Add(new GetUserData()
                        {
                            UserId = Convert.IsDBNull(reader["ID"]) ? 0 : Convert.ToInt32(reader["ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            IsActive = Convert.IsDBNull(reader["STATE"]) ? false : Convert.ToBoolean(reader["STATE"].ToString()),
                        });
                    }

                }
                int total = parameters.Get<int>("total");
                return new GetUsersQueryDTO()
                {
                    Users = response.ToArray(),
                    Total = total,
                    Page = command.Page,
                    Limit = command.Limit
                };
            }
        }

        public async Task<GetUserByIdQueryDTO> GetUserById(GetUserByIdQuery command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pId", command.Id, DbType.Int32, ParameterDirection.Input);

                GetUserByIdQueryDTO response = new GetUserByIdQueryDTO();

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetUsersById]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    while (reader.Read())
                    {
                        response = new GetUserByIdQueryDTO()
                        {
                            UserId = Convert.IsDBNull(reader["ID"]) ? 0 : Convert.ToInt32(reader["ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            Birthday = Convert.IsDBNull(reader["BIRTHDAY"]) ? default : Convert.ToDateTime(reader["BIRTHDAY"].ToString()),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            IsActive = Convert.IsDBNull(reader["STATE"]) ? false : Convert.ToBoolean(reader["STATE"].ToString()),
                            CreatedAt = Convert.IsDBNull(reader["CREATEDAT"]) ? default : Convert.ToDateTime(reader["CREATEDAT"].ToString()),
                        };
                    }
                }

                return response;
            }
        }
    }
}
