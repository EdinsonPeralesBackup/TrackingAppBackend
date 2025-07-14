using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact
{
    public class ChangeStatusConfidenceContactCommand : IRequest<ChangeStatusConfidenceContactCommandDTO>
    {
        public int Id { get; set; }
    }
}
