using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Domain.Repository.Stage;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
using DwapiCentral.Hts.Infrastructure.Persistence.Repository;
using DwapiCentral.Hts.Infrastructure.Persistence.Repository.Stage;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Z.Dapper.Plus;

namespace DwapiCentral.Hts.Infrastructure;

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
            services.AddDbContext<HtsDbContext>(x => x.UseSqlite(connection));
        }
        else
        {
            services.AddDbContext<HtsDbContext>(x => x.UseSqlServer(connectionString,
                o => o.MigrationsAssembly(typeof(HtsDbContext).Assembly.FullName)));
        }

        // add dependencies

        services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
        services.AddScoped<IFacilityRepository, FacilityRepository>();
        services.AddScoped<IManifestRepository, ManifestRepository>();
        services.AddScoped<IDocketRepository, DocketRepository>();
        services.AddScoped<IStageHtsClientRepository, StageHtsClientRepository>();
        services.AddScoped<IStageHtsClientTestRepository, StageHtsClientTestRepository>();
        services.AddScoped<IStageHtsClientLinkageRepository, StageHtsClientLinkageRepository>();
        services.AddScoped<IStageHtsTestKitRepository, StageHtsTestKitRepository>();
        services.AddScoped<IStageHtsClientTracingRepository, StageHtsClientTracingRepository>();
        services.AddScoped<IStageHtsPartnerTracingRepository, StageHtsPartnerTracingRepository>();
        services.AddScoped<IStageHtsPartnerNotificationServicesRepository, StageHtsPartnerNotificationServicesRepository>();
        services.AddScoped<IStageHtsEligibilityScreeningRepository, StageHtsEligibilityScreeningRepository>();




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