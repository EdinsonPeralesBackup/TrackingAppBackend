using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Application.Common.Interface;
using Tracking.Persistence.Database;

namespace Tracking.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDataBase>(sp => new SqlDataBase(connectionString));

            //services.AddSingleton<IAutenticacionRepository, AutenticacionRepository>();

            return services;
        }
    }
}
