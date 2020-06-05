using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application;
using SampleApp.Infrastructure.Persistence;
using System;

namespace SampleApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SampleAppDbContext>(options =>
            {
                options.UseSqlServer
                (
                    connectionString: configuration.GetConnectionString("SampleAppDBConStr"),
                    sqlServerOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly("SampleApp.DbMigration");
                    }
                );

                // options.EnableSensitiveDataLogging(true);
            });

            services.AddScoped<ISampleAppDbContext>(provider => provider.GetService<SampleAppDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<SampleAppDbContext>());

            return services;
        }
    }
}
