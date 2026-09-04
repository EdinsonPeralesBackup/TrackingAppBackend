using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Xml.Serialization;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Application.Maps.Command.ArriveRoute;
using Tracking.Application.Maps.Command.CancelRoute;
using Tracking.Application.Maps.Command.DangerRoute;
using Tracking.Application.Maps.Command.FinishDangerRoute;
using Tracking.Application.Maps.Command.ObtenerRuta;
using Tracking.Application.Maps.Command.UpdatePoint;
using Tracking.Application.Maps.Query.GetDangerRoute;
using Tracking.Application.Maps.Query.GetTrackingHistory;
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
                parameters.Add("@cancelable", false, DbType.Boolean, ParameterDirection.Output);
                parameters.Add("@isDanger", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_RegisterLiveCoordinate]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var status = parameters.Get<string>("status");
                var deviation = parameters.Get<double>("deviation");
                var lastLatitud = parameters.Get<double>("lastLatitud");
                var lastLongitute = parameters.Get<double>("lastLongitute");
                var cancelable = parameters.Get<bool>("cancelable");
                var danger = parameters.Get<string>("isDanger");
                return new UpdatePointCommandDTO()
                {
                    Status = status,
                    NextCheckIn = this.IntervalCheckpoint,
                    LastValidPoint = new Coordinates()
                    {
                        Latitude = lastLatitud,
                        Longitude = lastLongitute
                    },
                    DeviationRadius = deviation,
                    Cancelable = cancelable,
                    Danger = danger
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

        public async Task<IEnumerable<GetTrackingHistoryQueryDTO>> GetTrackingHistory(GetTrackingHistoryQuery query)
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
                            State = Convert.IsDBNull(reader["State"]) ? string.Empty : reader["State"].ToString()
                        });
                    }
                    return response;
                }
            }
        }

        public async Task<IEnumerable<CoordinatePointOfRoute>> GetPointOfRoute(int trackingId)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pTrackingId", trackingId, DbType.Int32, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetPointOfRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    List<CoordinatePointOfRoute> response = new();
                    while (reader.Read())
                    {
                        response.Add(new CoordinatePointOfRoute()
                        {
                            OriginLatitud = Convert.IsDBNull(reader["ORIGIN_LATITUD"]) ? 0 : Convert.ToDouble(reader["ORIGIN_LATITUD"].ToString()),
                            OriginLongitude = Convert.IsDBNull(reader["ORIGIN_LONGITUDE"]) ? 0 : Convert.ToDouble(reader["ORIGIN_LONGITUDE"].ToString()),
                            EndLatitud = Convert.IsDBNull(reader["END_LATITUD"]) ? 0 : Convert.ToDouble(reader["END_LATITUD"].ToString()),
                            EndLongitude = Convert.IsDBNull(reader["END_LONGITUDE"]) ? 0 : Convert.ToDouble(reader["END_LONGITUDE"].ToString())
                        });
                    }
                    return response;
                }
            }
        }

        public async Task<CancelRouteCommandDTO> CancelRoute(CancelRouteCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@puserId", command.IdUser, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@msj", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_CancelRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var message = parameters.Get<string>("msj");
                return new CancelRouteCommandDTO()
                {
                    Codigo = message
                };
            }
        }

        public async Task<DangerRouteCommandDTO> DangerRoute(DangerRouteCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@prouteId", command.TrackingId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ptimestampDangerFront", command.DateDangerFront, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@ptimestampDanger", command.DateDanger, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@msj", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_DangerRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var message = parameters.Get<string>("msj");
                return new DangerRouteCommandDTO()
                {
                    Mensaje = message
                };
            }
        }
        
        public async Task<IEnumerable<GetDangerRouteQueryDTO>> GetDangerRoute(GetDangerRouteQuery query)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pTrackingRoute", query.TrackingId, DbType.String, ParameterDirection.Input);
                parameters.Add("@pPhoneUser", query.Phone, DbType.String, ParameterDirection.Input);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_GetDangerRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    List<GetDangerRouteQueryDTO> response = new();
                    while (reader.Read())
                    {
                        response.Add(new GetDangerRouteQueryDTO()
                        {
                            Latitude = Convert.IsDBNull(reader["LATITUD"]) ? 0 : Convert.ToDouble(reader["LATITUD"].ToString()),
                            Longitude = Convert.IsDBNull(reader["LONGITUDE"]) ? 0 : Convert.ToDouble(reader["LONGITUDE"].ToString()),
                            Timestamp = Convert.IsDBNull(reader["TIME"]) ? default : Convert.ToDateTime(reader["TIME"].ToString()),
                        });
                    }
                    return response;
                }
            }
        }

        public async Task<FinishDangerRouteCommandDTO> FinishDangerRoute(FinishDangerRouteCommand command)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@pTrackingId", command.TrackingId, DbType.String, ParameterDirection.Input);
                parameters.Add("@pIdUser", command.IdUser, DbType.Int32, ParameterDirection.Input);

                parameters.Add("@finish", "", DbType.String, ParameterDirection.Output);

                using var reader = await cnx.ExecuteReaderAsync(
                    "[dbo].[sp_FinishDangerRoute]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var finish= parameters.Get<string>("finish");
                return new FinishDangerRouteCommandDTO()
                {
                    Finish = finish
                };
            }
        }
        private string ConvertirXML(List<Step> steps)
        {
            var segmentedSteps = new List<Step>();

            foreach (var step in steps)
            {
                var decodedPoints = DecodePolyline(step.Polyline?.Points);

                // Si por algún motivo Google no devuelve una polilínea válida,
                // conservamos el comportamiento anterior Start → End.
                if (decodedPoints.Count < 2)
                {
                    segmentedSteps.Add(step);
                    continue;
                }

                for (int i = 0; i < decodedPoints.Count - 1; i++)
                {
                    segmentedSteps.Add(new Step
                    {
                        Distance = step.Distance,
                        Duration = step.Duration,

                        Start_location = new Location
                        {
                            Lat = Math.Round(decodedPoints[i].Lat, 6),
                            Lng = Math.Round(decodedPoints[i].Lng, 6)
                        },

                        End_location = new Location
                        {
                            Lat = Math.Round(decodedPoints[i + 1].Lat, 6),
                            Lng = Math.Round(decodedPoints[i + 1].Lng, 6)
                        },

                        Html_instructions = step.Html_instructions,
                        Travel_mode = step.Travel_mode
                    });
                }
            }

            var serializer = new XmlSerializer(typeof(List<Step>));

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, segmentedSteps);
                return writer.ToString();
            }
        }
        private List<Location> DecodePolyline(string? encodedPoints)
        {
            var points = new List<Location>();

            if (string.IsNullOrWhiteSpace(encodedPoints))
                return points;

            int index = 0;
            int latitude = 0;
            int longitude = 0;

            while (index < encodedPoints.Length)
            {
                int result = 0;
                int shift = 0;
                int b;

                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                }
                while (b >= 0x20 && index < encodedPoints.Length);

                int deltaLatitude =
                    (result & 1) != 0 ? ~(result >> 1) : (result >> 1);

                latitude += deltaLatitude;

                result = 0;
                shift = 0;

                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                }
                while (b >= 0x20 && index < encodedPoints.Length);

                int deltaLongitude =
                    (result & 1) != 0 ? ~(result >> 1) : (result >> 1);

                longitude += deltaLongitude;

                points.Add(new Location
                {
                    Lat = latitude / 100000.0,
                    Lng = longitude / 100000.0
                });
            }

            return points;
        }
    }
}
