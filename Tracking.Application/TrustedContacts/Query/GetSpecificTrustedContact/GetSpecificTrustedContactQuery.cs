using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact
{
    public class GetSpecificTrustedContactQuery : IRequest<GetSpecificTrustedContactQueryDTO>
    {
        public int IdTrustedContact { get; set; }
    }
}
