using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;

namespace Tracking.Application.Maps.Command.ArriveRoute
{
    public class ArriveRouteCommandDTO
    {
        public string Message { get; set; }
        public List<GetTrustedContactQueryDTO> ContactsNotified { get; set; }
    }
}
