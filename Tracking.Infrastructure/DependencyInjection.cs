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
            services.AddSingleton<ITwilioService, TwilioService>();
            services.AddSingleton<IMapsServices, MapsServices>();
            services.AddHttpClient<IAcortadorServices, AcortadorServices>();
            return services;
        }

    }
}
