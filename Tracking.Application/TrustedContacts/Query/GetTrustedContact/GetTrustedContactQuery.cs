using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.TrustedContacts.Query.GetTrustedContact
{
    public class GetTrustedContactQuery : IRequest<IEnumerable<GetTrustedContactQueryDTO>>
    {
        public int IdUser { get; set; }
    }
}
