using Microsoft.Extensions.DependencyInjection;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Interface.Repositories;
using Tracking.Persistence.Database;
using Tracking.Persistence.Repository;

namespace Tracking.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDataBase>(sp => new SqlDataBase(connectionString));

            services.AddSingleton<IAuthorizationRepository, AuthorizationRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITrustedContactRepository, TrustedContactRepository>();
            services.AddSingleton<IMapsRepository, MapsRepository>();

            return services;
        }
    }
}
