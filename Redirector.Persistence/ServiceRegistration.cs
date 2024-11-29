using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Redirector.Persistence.Contexts;
using Redirector.Persistence.Repositories;
using Redirector.Application.Interfaces;

namespace Redirector.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            if (Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") != null)
                connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
            services.AddDbContext<ModelContext>((provider, options) =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                options
                    .UseNpgsql(connectionString)
                    .UseLoggerFactory(loggerFactory)
                    .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.CommandError));
            });

            #region Repositories
            services.AddScoped<IGetSmartLinksRepository, GetSmartLinksRepository>();
            #endregion
        }
    }
}
