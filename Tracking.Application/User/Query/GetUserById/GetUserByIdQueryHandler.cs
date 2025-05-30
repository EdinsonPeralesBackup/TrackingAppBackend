using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tracking.Application.Common.Interface.Repositories;

namespace Tracking.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryDTO>
    {
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(
            ILogger<GetUserByIdQueryHandler> logger,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public Task<GetUserByIdQueryDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = this._userRepository.GetUserById(request);
            return response;
        }
    }
}
