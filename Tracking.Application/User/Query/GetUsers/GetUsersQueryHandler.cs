using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryDTO>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(
            ILogger<GetUsersQueryHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<GetUsersQueryDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.GetUser(request);
            return response;
        }
    }
}
