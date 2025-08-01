using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Authorization.Commad.Register;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Persistence.Database;

namespace Tracking.Persistence.Repository
{
    public class MapsRepository : IMapsRepository
    {
        private readonly IDataBase _dataBase;
        private readonly IDateTimeService _dateTimeService;
        private int apiKey;

        public MapsRepository(
            IServiceProvider serviceProvider, 
            IDateTimeService dateTimeService
            )
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
            this._dateTimeService = dateTimeService;
            this.apiKey = 3600;
        }

        public async Task<ObtenerRutaCommandDTO> RegisterRoute(Route command, int IdUser)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pdistance_text", command.Distance.Text, DbType.String, ParameterDirection.Input);
                parameters.Add("@pdistance", command.Distance.Value, DbType.Double, ParameterDirection.Input);

                parameters.Add("@pduration_text", command.Duration.Text, DbType.String, ParameterDirection.Input);
                parameters.Add("@pduration", command.Duration.Value, DbType.Double, ParameterDirection.Input);

                parameters.Add("@porigin_address", command.StartAddress, DbType.String, ParameterDirection.Input);
                parameters.Add("@porigin_latitud", command.StartLocation.Lat, DbType.Double, ParameterDirection.Input);
                parameters.Add("@porigin_longitude", command.StartLocation.Lng, DbType.Double, ParameterDirection.Input);

                parameters.Add("@pdestination_address", command.EndAddress, DbType.String, ParameterDirection.Input);
                parameters.Add("@pdestination_latitud", command.EndLocation.Lat, DbType.Double, ParameterDirection.Input);
                parameters.Add("@pdestination_longitude", command.EndLocation.Lng, DbType.Double, ParameterDirection.Input);

                parameters.Add("@pidUser", IdUser, DbType.Int32, ParameterDirection.Input);

                parameters.Add("@ptimestamp", this._dateTimeService.HoraActual(), DbType.DateTime, ParameterDirection.Input);

                parameters.Add("@pXMLPoint", "", DbType.Xml, ParameterDirection.Input);

                parameters.Add("@numberTracking", "", DbType.String, ParameterDirection.Output);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var trackingId = parameters.Get<string>("numberTracking");
                var message = parameters.Get<string>("message");
                return new ObtenerRutaCommandDTO()
                {
                    RouteTravel = command,
                    TrackingId = trackingId,
                    Message = message,
                    CheckpointInterval = this.apiKey
                };
            }
        }
    }
}
