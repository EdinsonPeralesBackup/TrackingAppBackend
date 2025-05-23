using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Application.Common.Interface;
using Tracking.Infrastructure.Services;

namespace Tracking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<ICryptography, Cryptography>();
            services.AddSingleton<IJwtService, JwtService>();
            return services;
        }

    }
}
