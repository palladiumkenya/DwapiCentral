using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
using DwapiCentral.Mnch.Infrastructure.Persistence.Repository;
using DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Z.Dapper.Plus;

namespace DwapiCentral.Mnch.Infrastructure;

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
            services.AddDbContext<MnchDbContext>(x => x.UseSqlite(connection));
        }
        else
        {
            services.AddDbContext<MnchDbContext>(x => x.UseSqlServer(connectionString,
                o => o.MigrationsAssembly(typeof(MnchDbContext).Assembly.FullName)));
        }

        // add dependencies

        services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
        services.AddScoped<IFacilityRepository, FacilityRepository>();
        services.AddScoped<IManifestRepository, ManifestRepository>();
        services.AddScoped<IDocketRepository, DocketRepository>();


        services.AddScoped<IStageAncVisitRepository, StageAncVisitRepository>();
        services.AddScoped<IStageCwcEnrolmentRepository, StageCwcEnrolmentRepository>();
        services.AddScoped<IStageCwcVisitRepository, StageCwcVisitRepository>();
        services.AddScoped<IStageHeiExtractRepository, StageHeiExtractRepository>();
        services.AddScoped<IStageMatVisitRepository, StageMatVisitRepository>();
        services.AddScoped<IStageMnchArtRepository, StageMnchArtRepository>();
        services.AddScoped<IStageMnchEnrolmentRepository, StageMnchEnrolmentRepository>();
        services.AddScoped<IStageMnchImmunizationRepository, StageMnchImmunizationRepository>();
        services.AddScoped<IStageMnchLabRepository, StageMnchLabRepository>();
        services.AddScoped<IStageMotherBabyPairRepository, StageMotherBabyPairRepository>();
        services.AddScoped<IStagePatientMnchRepository, StagePatientMnchRepository>();
        services.AddScoped<IStagePncVisitRepository, StagePncVisitRepository>();

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