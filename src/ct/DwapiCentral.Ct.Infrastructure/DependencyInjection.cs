using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PalladiumDwh.Infrastructure.Data.Repository.Stage;
using Serilog;
using Z.Dapper.Plus;

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
        services.AddScoped<IPatientExtractRepository, PatientExtractRepository>();
        services.AddScoped<IPatientVisitExtractRepository, PatientVisitExtractRepository>();
        services.AddScoped<IPatientPharmacyRepository, PatientPharmacyRepository>();
        services.AddScoped<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>();
        services.AddScoped<IPatientArtExtractRepositorycs, PatientArtExtractRepository>();
        services.AddScoped<IAllergiesChronicIllnessRepository,AllergiesChronicIllnessRepository>();
        services.AddScoped<IContactListingRepository, ContactListingRepository>();
        services.AddScoped<ICovidRepository, CovidRepository>();
        services.AddScoped<IDefaulterTracingRepository, DefaulterTracingRepository>();
        services.AddScoped<IDepressionScreeningRepository, DepressionScreeningRepository>();
        services.AddScoped<IDrugAlcoholScreeningRepository, DrugAlcoholScreeningRepository>();
        services.AddScoped<IEnhancedAdherenceCounsellingRepository, EnhancedAdherenceCounsellingRepository>();
        services.AddScoped<IGbvScreeningRepository, GbvScreeningRepository>();
        services.AddScoped<IIptRepository, IptRepository>();
        services.AddScoped<IOvcRepository, OvcRepository>();
        services.AddScoped<IOtzRepository, OtzRepository>();
        services.AddScoped<IPatientAdverseEventRepository, PatientAdverseEventRepository>();
        services.AddScoped<IPatientBaseLinesRepository, PatientBaseLinesRepository>();
        services.AddScoped<IPatientStatusRepository, PatientStatusRepository>();
        

        services.AddScoped<IStagePatientExtractRepository, StagePatientExtractRepository>();
        services.AddScoped<IStageVisitExtractRepository, StageVisitExtractRepository>();
        services.AddScoped<IStagePharmacyExtractRepository, StagePharmacyExtractRepository>();
        services.AddScoped<IStageLaboratoryExtractRepository, StageLaboratoryExtractRepository>();
        services.AddScoped<IStageArtExtractRepository, StageArtExtractRepository>();
        services.AddScoped<IStageAllergiesChronicIllnessExtractRepository, StageAllergiesChronicIllnessExtractRepository>();
        services.AddScoped<IStageContactListingExtractRepository, StageContactListingExtractRepository>();
        services.AddScoped<IStageCovidExtractRepository, StageCovidExtractRepository>();
        services.AddScoped<IStageDefaulterTracingExtractRepository, StageDefaulterTracingExtractRepository>();
        services.AddScoped<IStageDepressionScreeningExtractRepository, StageDepressionScreeningExtractRepository>();
        services.AddScoped<IStageDrugAlcoholScreeningExtractRepository, StageDrugAlcoholScreeningExtractRepository>();
        services.AddScoped<IStageEnhancedAdherenceCounsellingExtractRepository, StageEnhancedAdherenceCounsellingExtractRepository>();
        services.AddScoped<IStageGbvScreeningExtractRepository, StageGbvScreeningExtractRepository>();
        services.AddScoped<IStageIptExtractRepository, StageIptExtractRepository>();
        services.AddScoped<IStageOvcExtractRepository, StageOvcExtractRepository>();
        services.AddScoped<IStageOtzExtractRepository, StageOtzExtractRepository>();
        services.AddScoped<IStageAdverseEventExtractRepository, StageAdverseEventExtractRepository>();
        services.AddScoped<IStageBaselineExtractRepository, StageBaselineExtractRepository>();
        services.AddScoped<IStageStatusExtractRepository, StageStatusExtractRepository>();
        services.AddScoped<IStageCervicalCancerScreeningExtractsRepository, StageCervicalCancerScreeningExtractsRepository>();
        services.AddScoped<IStageIITRiskScoreRepository, StageIITRiskScoreRepository>();


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