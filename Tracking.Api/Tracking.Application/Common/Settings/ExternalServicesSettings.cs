using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Common.Settings
{
    public class ExternalServicesSettings
    {
        public IEnumerable<ExternalServices> ExternalServices { get; set; }
    }

    public class ExternalServices
    {
        public string Nombre { get; set; }
        public string UrlBase { get; set; }
        public string ApiKey { get; set; }
        public IEnumerable<Endpoints> Endpoints { get; set; }
    }

    public class Endpoints
    {
        public string Nombre { get; set; }
        public string Endpoint { get; set; }
    }
}
