using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.User.Query.GetUsers
{
    public class GetUsersQuery : IRequest<GetUsersQueryDTO>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
