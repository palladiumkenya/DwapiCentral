using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration,bool isSqlite=false,string dynamicConnectionString="")
    {
        var connectionString = configuration.GetConnectionString("LiveConnection");
        if (isSqlite)
        {
            if (!string.IsNullOrWhiteSpace(dynamicConnectionString))
                connectionString = dynamicConnectionString;

            var connection = new SqliteConnection(connectionString);
            connection.Open();
            services.AddDbContext<CtDbContext>(x => x.UseSqlite(connection));
        }
        else
        {
            services.AddDbContext<CtDbContext>(x => x.UseSqlServer(connectionString,
                o => o.MigrationsAssembly(typeof(CtDbContext).Assembly.FullName)));
        }

        // add dependencies
        
        services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
        services.AddScoped<IFacilityRepository, FacilityRepository>();
        services.AddScoped<IManifestRepository, ManifestRepository>();
        
        return services;
    }
}