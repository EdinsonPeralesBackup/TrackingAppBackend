using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.Authorization.Commad.ValidToken
{
    public class ValidTokenCommand : IRequest<ValidTokenCommandDTO>
    {
        public string Token { get; set; }
    }
}
