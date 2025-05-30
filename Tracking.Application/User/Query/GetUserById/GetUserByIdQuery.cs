using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryDTO>
    {
        public int Id { get; set; }
    }
}
