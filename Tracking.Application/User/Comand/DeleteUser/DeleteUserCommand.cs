using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tracking.Application.User.Comand.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserCommandDTO>
    {
        public int IdUser { get; set; }
    }
}
