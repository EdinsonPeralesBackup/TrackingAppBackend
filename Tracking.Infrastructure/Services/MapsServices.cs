using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;
using Tracking.Application.Maps.Command.ObtenerRuta;

namespace Tracking.Infrastructure.Services
{
    public class MapsServices : IMapsServices
    {
        private readonly IConfiguration _configuration;
        private string apiKey;

        public MapsServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.apiKey = this._configuration.GetValue<string>("ApiKeyMaps");
        }
        public async Task<string> ObtenerRuta(RegisterRouteCommand command)
        {
            using var client = new HttpClient();
            string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={(command.Origin.Lat).ToString(CultureInfo.InvariantCulture)},{(command.Origin.Lng).ToString(CultureInfo.InvariantCulture)}&destination={(command.Destination.Lat).ToString(CultureInfo.InvariantCulture)},{(command.Destination.Lng).ToString(CultureInfo.InvariantCulture)}&key={apiKey}";
            var response = await client.GetStringAsync(url);
            return response;
        }
    }
}
