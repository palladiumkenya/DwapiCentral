using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
using DwapiCentral.Prep.Infrastructure.Persistence.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Z.Dapper.Plus;

namespace DwapiCentral.Prep.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isSqlite = false, string dynamicConnectionString = "")
    {
        var connectionString = configuration.GetConnectionString("LiveConnection");
        if (isSqlite)
        {
            if (!string.IsNullOrWhiteSpace(dynamicConnectionString))
                connectionString = dynamicConnectionString;

            var connection = new SqliteConnection(connectionString);
            connection.Open();
            services.AddDbContext<PrepDbContext>(x => x.UseSqlite(connection));
        }
        else
        {
            services.AddDbContext<PrepDbContext>(x => x.UseSqlServer(connectionString,
                o => o.MigrationsAssembly(typeof(PrepDbContext).Assembly.FullName)));
        }

        // add dependencies

        services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
        services.AddScoped<IFacilityRepository, FacilityRepository>();
        services.AddScoped<IManifestRepository, ManifestRepository>();
        services.AddScoped<IDocketRepository, DocketRepository>();
        




        try
        {
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
            if (!DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
        }
        catch (Exception e)
        {
            Log.Debug($"{e}");
            throw;
        }



        return services;
    }
}