using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracking.Application.Authorization.Commad.Register;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetTrackingHistory;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;
using Tracking.Persistence.Database;

namespace Tracking.Persistence.Repository
{
    public class MapsRepository : IMapsRepository
    {
        private readonly IDataBase _dataBase;
        private readonly IDateTimeService _dateTimeService;
        private int IntervalCheckpoint;

        public MapsRepository(
            IServiceProvider serviceProvider,
            IDateTimeService dateTimeService
            )
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
            this._dateTimeService = dateTimeService;
            this.IntervalCheckpoint = 30;
        }

        public async Task<RegisterRouteCommandDTO> RegisterRoute(Route command, int IdUser, int RouteCalibrated)
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

                parameters.Add("@pXMLPoint", this.ConvertirXML(command.Steps), DbType.Xml, ParameterDirection.Input);

                parameters.Add("@pRouteCalibrated", RouteCalibrated, DbType.Int32, ParameterDirection.Input);

                parameters.Add("@idRoute", "", DbType.Int32, ParameterDirection.Output);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var trackingId = parameters.Get<Int32>("idRoute");
                var message = parameters.Get<string>("message");
                return new RegisterRouteCommandDTO()
                {
                    RouteTravel = command,
                    TrackingId = trackingId,
                    Message = message,
                    CheckpointInterval = this.IntervalCheckpoint
                };
            }
        }

        public async Task<UpdatePointCommandDTO> UpdatePoint(UpdatePointCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pidTracking", command.TrackingId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@platitud", command.Coordinates.Latitude, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@plongitute", command.Coordinates.Longitude, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@ptimestamp", this._dateTimeService.HoraActual(), DbType.DateTime, ParameterDirection.Input);

                parameters.Add("@status", "", DbType.String, ParameterDirection.Output);
                parameters.Add("@deviation", 0, DbType.Double, ParameterDirection.Output);
                parameters.Add("@lastLatitud", 0, DbType.Double, ParameterDirection.Output);
                parameters.Add("@lastLongitute", 0, DbType.Double, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterLiveCoordinate]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var status = parameters.Get<string>("status");
                var deviation = parameters.Get<double>("deviation");
                var lastLatitud = parameters.Get<double>("lastLatitud");
                var lastLongitute = parameters.Get<double>("lastLongitute");
                return new UpdatePointCommandDTO()
                {
                    Status = status,
                    NextCheckIn = this.IntervalCheckpoint,
                    LastValidPoint = new Coordinates()
                    {
                        Latitude = lastLatitud,
                        Longitude = lastLongitute
                    },
                    DeviationRadius = deviation
                };
            }
        }

        public async Task<ArriveRouteCommandDTO> ArriveRoute(ArriveRouteCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@ptrackingID", command.TrackingId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@puserId", command.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@plongitude", command.Coordinates.Longitude, DbType.Double, ParameterDirection.Input);
                parameters.Add("@platitude", command.Coordinates.Latitude, DbType.Double, ParameterDirection.Input);
                parameters.Add("@message", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_ArriveRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var message = parameters.Get<string>("message");
                return new ArriveRouteCommandDTO()
                {
                    Message = message
                };
            }
        }

        public async Task<IEnumerable<GetTrackingHistoryQueryDTO>> GetTrustedContacts(GetTrackingHistoryQuery query)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pIdUser", query.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pEsRutaActual", query.EsRutaActual, DbType.Boolean, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetTrackingHistory]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    List<GetTrackingHistoryQueryDTO> response = new();
                    while (reader.Read())
                    {
                        response.Add(new GetTrackingHistoryQueryDTO()
                        {
                            IdRoute = Convert.IsDBNull(reader["ID_ROUTE"]) ? 0 : Convert.ToInt32(reader["ID_ROUTE"].ToString()),
                            Origen = Convert.IsDBNull(reader["ORIGEN"]) ? string.Empty : reader["ORIGEN"].ToString(),
                            OrigenLatitud = Convert.IsDBNull(reader["ORIGIN_LATITUD"]) ? 0 : Convert.ToDouble(reader["ORIGIN_LATITUD"].ToString()),
                            OrigenLongitude = Convert.IsDBNull(reader["ORIGIN_LONGITUD"]) ? 0 : Convert.ToDouble(reader["ORIGIN_LONGITUD"].ToString()),
                            Destination = Convert.IsDBNull(reader["DESTINATION"]) ? string.Empty : reader["DESTINATION"].ToString(),
                            DestinationLatitud = Convert.IsDBNull(reader["DESTINATION_LATITUD"]) ? 0 : Convert.ToDouble(reader["DESTINATION_LATITUD"].ToString()),
                            DestinationLongitude = Convert.IsDBNull(reader["DESTINATION_LONGITUDE"]) ? 0 : Convert.ToDouble(reader["DESTINATION_LONGITUDE"].ToString()),
                            Timestamp = Convert.IsDBNull(reader["TIME"]) ? default : Convert.ToDateTime(reader["TIME"].ToString()),
                        });
                    }
                    return response;
                }
            }
        }

        private string ConvertirXML(List<Step> steps)
        {
            var serializer = new XmlSerializer(typeof(List<Step>));
            string xmlOutput;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, steps);
                xmlOutput = writer.ToString();
                Console.WriteLine(xmlOutput);
            }
            return xmlOutput;
        }
    }
}
