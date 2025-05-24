using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Application.Authorization.Commad.Login;
using Tracking.Application.Authorization.Commad.Register;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Persistence.Database;

namespace Tracking.Persistence.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IDataBase _dataBase;

        private readonly ICryptography cryptography;

        public AuthorizationRepository(IServiceProvider serviceProvider, ICryptography cryptography)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
            this.cryptography = cryptography;
        }

        public async Task<LoginCommandDTO> Login(LoginCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pphone", command.Phone, DbType.String, ParameterDirection.Input);
                parameters.Add("@pclave", this.cryptography.Encrypt(command.Password), DbType.String, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_login]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    LoginCommandDTO response = new();
                    while (reader.Read())
                    {
                        response = new LoginCommandDTO()
                        {
                            Id = Convert.IsDBNull(reader["ID"]) ? 0 : Convert.ToInt32(reader["ID"].ToString()),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            Lastname = Convert.IsDBNull(reader["LAST_NAME"]) ? "" : reader["LAST_NAME"].ToString(),
                            Birthday = Convert.IsDBNull(reader["BIRTHDAY"]) ? "" : reader["BIRTHDAY"].ToString(),
                            IdRole = Convert.IsDBNull(reader["ID_ROLE"]) ? 0 : Convert.ToInt32(reader["ID_ROLE"].ToString()),
                            Role = Convert.IsDBNull(reader["ROLE"]) ? "" : reader["ROLE"].ToString()
                        };
                    }
                    return response;
                }
            }
        }

        public async Task<RegisterCommandDTO> Register(RegisterCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pname", command.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@plastName", command.Lastname, DbType.String, ParameterDirection.Input);
                parameters.Add("@pbirthday", Convert.ToDateTime(command.Birthday), DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@ppassword", this.cryptography.Encrypt(command.Password), DbType.String, ParameterDirection.Input);
                parameters.Add("@pphonenumber", command.Phonenumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@idUser", "", DbType.Int32, ParameterDirection.Output);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterUser]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var userId = parameters.Get<int>("idUser");
                var message = parameters.Get<string>("message");
                return new RegisterCommandDTO()
                {
                    UserId = userId,
                    Message = message
                };
            }
        }
    }
}
