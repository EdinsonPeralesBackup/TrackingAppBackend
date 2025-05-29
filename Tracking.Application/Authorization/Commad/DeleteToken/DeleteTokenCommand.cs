using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.Authorization.Commad.DeleteToken
{
    public class DeleteTokenCommand : IRequest<DeleteTokenCommandDTO>
    {
        public int IdUser { get; set; }
    }
}
