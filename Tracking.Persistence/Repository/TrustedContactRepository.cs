using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.Maps.Command.SendSOSSignal;
using Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact;
using Tracking.Application.TrustedContacts.Command.DeleteTrustedContact;
using Tracking.Application.TrustedContacts.Command.RegisterTrustedContact;
using Tracking.Application.TrustedContacts.Command.RegisterVisit;
using Tracking.Application.TrustedContacts.Command.UpdateTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;
using Tracking.Persistence.Database;

namespace Tracking.Persistence.Repository
{
    public class TrustedContactRepository : ITrustedContactRepository
    {
        private readonly IDataBase _dataBase;
        private readonly IDateTimeService _dateTimeService;

        public TrustedContactRepository(IServiceProvider serviceProvider, IDateTimeService dateTimeService)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
            this._dateTimeService = dateTimeService;
        }
        public async Task<RegisterTrustedContactCommandDTO> RegisterTrustedContacts(RegisterTrustedContactCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pname", command.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@plastName", command.LastName, DbType.String, ParameterDirection.Input);
                parameters.Add("@pphone", command.Phone, DbType.String, ParameterDirection.Input);
                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pidUserCreate", command.IdUserCreate, DbType.String, ParameterDirection.Input);
                parameters.Add("@pdateCreate", this._dateTimeService.HoraLocal(), DbType.DateTime, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    RegisterTrustedContactCommandDTO response = new();
                    while (reader.Read())
                    {
                        response = new RegisterTrustedContactCommandDTO()
                        {
                            Message = Convert.IsDBNull(reader["MESSAGE"]) ? "" : reader["MESSAGE"].ToString(),
                            ContactId = Convert.IsDBNull(reader["CONTACT_ID"]) ? 0 : Convert.ToInt32(reader["CONTACT_ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            Status = Convert.IsDBNull(reader["STATUS"]) ? false : Convert.ToBoolean(reader["STATUS"].ToString())
                        };
                    }
                    return response;
                }
            }
        }

        public async Task<IEnumerable<GetTrustedContactQueryDTO>> GetTrustedContacts(GetTrustedContactQuery command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    List<GetTrustedContactQueryDTO> response = new();
                    while (reader.Read())
                    {
                        response.Add(new GetTrustedContactQueryDTO()
                        {
                            ContactId = Convert.IsDBNull(reader["CONTACT_ID"]) ? 0 : Convert.ToInt32(reader["CONTACT_ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            Status = Convert.IsDBNull(reader["STATUS"]) ? false : Convert.ToBoolean(reader["STATUS"].ToString())
                        });
                    }
                    return response;
                }
            }
        }

        public async Task<GetSpecificTrustedContactQueryDTO> GetSpecificTrustedContacts(GetSpecificTrustedContactQuery command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pidTrustedContact", command.IdTrustedContact, DbType.Int32, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetSpecificConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    GetSpecificTrustedContactQueryDTO response = new();
                    while (reader.Read())
                    {
                        response = new GetSpecificTrustedContactQueryDTO()
                        {
                            ContactId = Convert.IsDBNull(reader["CONTACT_ID"]) ? 0 : Convert.ToInt32(reader["CONTACT_ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            Status = Convert.IsDBNull(reader["STATUS"]) ? false : Convert.ToBoolean(reader["STATUS"].ToString())
                        };
                    }
                    return response;
                }
            }
        }

        public async Task<UpdateTrustedContactCommandDTO> UpdateTrustedContacts(UpdateTrustedContactCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pid", command.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pname", command.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@plastName", command.LastName, DbType.String, ParameterDirection.Input);
                parameters.Add("@pphone", command.Phone, DbType.String, ParameterDirection.Input);
                parameters.Add("@pidUserUpdate", command.IdUserUpdate, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pdateUpdate", this._dateTimeService.HoraLocal(), DbType.DateTime, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_UpdateConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    UpdateTrustedContactCommandDTO response = new();
                    while (reader.Read())
                    {
                        response = new UpdateTrustedContactCommandDTO()
                        {
                            Message = Convert.IsDBNull(reader["MESSAGE"]) ? "" : reader["MESSAGE"].ToString(),
                            ContactId = Convert.IsDBNull(reader["CONTACT_ID"]) ? 0 : Convert.ToInt32(reader["CONTACT_ID"].ToString()),
                            Name = Convert.IsDBNull(reader["NAME"]) ? "" : reader["NAME"].ToString(),
                            LastName = Convert.IsDBNull(reader["LASTNAME"]) ? "" : reader["LASTNAME"].ToString(),
                            Phone = Convert.IsDBNull(reader["PHONE"]) ? "" : reader["PHONE"].ToString(),
                            Status = Convert.IsDBNull(reader["STATUS"]) ? false : Convert.ToBoolean(reader["STATUS"].ToString())
                        };
                    }
                    return response;
                }
            }
        }

        public async Task<DeleteTrustedContactCommandDTO> DeleteTrustedContacts(DeleteTrustedContactCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pid", command.IdTrustedContact, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_DeleteConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("message");
                return new DeleteTrustedContactCommandDTO()
                {
                    Message = mensaje
                };
            }
        }

        public async Task<ChangeStatusConfidenceContactCommandDTO> ChangeStatusTrustedContacts(ChangeStatusConfidenceContactCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pid", command.Id, DbType.Int32, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_ChangeStatusConfidenceContact]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    ChangeStatusConfidenceContactCommandDTO response = new();
                    while (reader.Read())
                    {
                        response = new ChangeStatusConfidenceContactCommandDTO()
                        {
                            Message = Convert.IsDBNull(reader["MESSAGE"]) ? "" : reader["MESSAGE"].ToString(),
                            ContactId = Convert.IsDBNull(reader["CONTACT_ID"]) ? 0 : Convert.ToInt32(reader["CONTACT_ID"].ToString()),
                            Status = Convert.IsDBNull(reader["IS_ACTIVE"]) ? false : Convert.ToBoolean(reader["IS_ACTIVE"].ToString())
                        };
                    }
                    return response;
                }
            }
        }

        public async Task<string> RegisterAlert(RegisterAlert command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pidUser", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ptrackingId", command.TrackingId, DbType.String, ParameterDirection.Input);
                parameters.Add("@platitude", command.Coordinate.Latitude, DbType.Double, ParameterDirection.Input);
                parameters.Add("@plongitude", command.Coordinate.Longitude, DbType.Double, ParameterDirection.Input);
                parameters.Add("@ptimestamp", command.DateRegister, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@msj", "", DbType.String, ParameterDirection.Output);

                await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterAlert]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("msj");
                return mensaje;
            }
        }

        public async Task<RegisterVisitCommandDTO> RegisterVisit(RegisterVisitCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pTrackingId", command.TrackingId, DbType.String, ParameterDirection.Input);
                parameters.Add("@msj", "", DbType.String, ParameterDirection.Output);

                await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterVisit]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var mensaje = parameters.Get<string>("msj");
                return new RegisterVisitCommandDTO()
                {
                    Mensaje = mensaje
                };
            }
        }
    }
}
