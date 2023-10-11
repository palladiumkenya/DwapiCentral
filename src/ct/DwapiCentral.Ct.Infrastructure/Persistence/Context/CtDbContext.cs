using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;

using DwapiCentral.Ct.Domain.Models.Stage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Context
{
    public class CtDbContext : DbContext
    {
      

        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<PatientExtract> PatientExtract { get; set; }  
        public DbSet<PatientVisitExtract> PatientVisitExtract { get; set; }
        public DbSet<PatientPharmacyExtract> PatientPharmacyExtract { get; set; }
        public DbSet<PatientLaboratoryExtract> PatientLaboratoryExtract { get; set; }
        public DbSet<PatientArtExtract> PatientArtExtract { get; set; }
        public DbSet<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtract { get; set; }
        public DbSet<ContactListingExtract> ContactListingExtract { get; set; }
        public DbSet<CovidExtract> CovidExtract { get; set; }
        public DbSet<DefaulterTracingExtract> DefaulterTracingExtract { get; set; }
        public DbSet<DepressionScreeningExtract> DepressionScreeningExtract { get; set; }
        public DbSet<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtract { get; set; } 
        public DbSet<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtract { get; set; }
        public DbSet<GbvScreeningExtract> GbvScreeningExtract { get; set; }
        public DbSet<IptExtract> IptExtract { get; set; }
        public DbSet<OvcExtract> OvcExtract { get; set; }
        public DbSet<OtzExtract> OtzExtract { get; set; }
        public DbSet<PatientAdverseEventExtract> PatientAdverseEventExtract { get; set; } 
        public DbSet<PatientBaselinesExtract> PatientBaselinesExtract { get; set; }
        public DbSet<PatientStatusExtract> PatientStatusExtract { get; set; }
        public DbSet<CervicalCancerScreeningExtract> CervicalCancerScreeningExtract { get; set; }
        public virtual DbSet<IITRiskScore> IITRiskScoresExtract { get; set; }

        public virtual DbSet<StagePatientExtract> StagePatientExtracts { get; set; }
        public virtual DbSet<StageVisitExtract> StageVisitExtracts { get; set; }
        public virtual DbSet<StageAdverseEventExtract> StageAdverseEventExtracts { get; set; }
        public virtual DbSet<StageAllergiesChronicIllnessExtract> StageAllergiesChronicIllnessExtracts { get; set; }
        public virtual DbSet<StageArtExtract> StageArtExtracts { get; set; }
        public virtual DbSet<StageBaselineExtract> StageBaselineExtracts { get; set; }
        public virtual DbSet<StageContactListingExtract> StageContactListingExtracts { get; set; }
        public virtual DbSet<StageCovidExtract> StageCovidExtracts { get; set; }
        public virtual DbSet<StageDefaulterTracingExtract> StageDefaulterTracingExtracts { get; set; }
        public virtual DbSet<StageDepressionScreeningExtract> StageDepressionScreeningExtracts { get; set; }
        public virtual DbSet<StageDrugAlcoholScreeningExtract> StageDrugAlcoholScreeningExtracts { get; set; }
        public virtual DbSet<StageEnhancedAdherenceCounsellingExtract> StageEnhancedAdherenceCounsellingExtracts { get; set; }
        public virtual DbSet<StageIptExtract> StageIptExtracts { get; set; }
        public virtual DbSet<StageLaboratoryExtract> StageLaboratoryExtracts { get; set; }
        public virtual DbSet<StageOtzExtract> StageOtzExtracts { get; set; }
        public virtual DbSet<StageOvcExtract> StageOvcExtracts { get; set; }
        public virtual DbSet<StagePharmacyExtract> StagePharmacyExtracts { get; set; }
        public virtual DbSet<StageStatusExtract> StageStatusExtracts { get; set; }
        public virtual DbSet<StageGbvScreeningExtract> StageGbvScreeningExtracts { get; set; }
        public virtual DbSet<StageCervicalCancerScreeningExtract> StageCervicalCancerScreeningExtracts { get; set; }
        public virtual DbSet<StageIITRiskScore> StageIITRiskScoresExtracts { get; set; }

        public CtDbContext(DbContextOptions<CtDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientExtract>()               
                 .HasKey(m => new { m.PatientPk, m.SiteCode });
            
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientVisitExtracts)
                .WithOne()
                .HasForeignKey(f =>new {f.PatientPk,f.SiteCode})
                .IsRequired();

            modelBuilder.Entity<StagePatientExtract>()
               .HasKey(m => new { m.PatientPk, m.SiteCode });


            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientPharmacyExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientLaboratoryExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientArtExtracts)
                .WithOne()
                .HasForeignKey(p => new {p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.AllergiesChronicIllnessExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.ContactListingExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.CovidExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.DefaulterTracingExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
               .HasMany(p => p.DepressionScreeningExtracts)
               .WithOne()
               .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
               .IsRequired();

            modelBuilder.Entity<PatientExtract>()
              .HasMany(p => p.DrugAlcoholScreeningExtracts)
              .WithOne()
              .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
              .IsRequired();

            modelBuilder.Entity<PatientExtract>()
              .HasMany(p => p.EnhancedAdherenceCounsellingExtracts)
              .WithOne()
              .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
              .IsRequired();

            modelBuilder.Entity<PatientExtract>()
              .HasMany(p => p.GbvScreeningExtracts)
              .WithOne()
              .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
              .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.IptExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.OvcExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.OtzExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientAdverseEventExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientBaselinesExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientStatusExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.CervicalCancerScreeningExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.IITRiskScoresExtracts)
               .WithOne()
                .HasForeignKey(p => new { p.PatientPk, p.SiteCode })
                .IsRequired();

            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");

            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");
            
            DapperPlusManager.Entity<Metric>().Key(x => x.Id).Table($"{nameof(Metrics)}");
            
            DapperPlusManager.Entity<PatientExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(PatientExtract)}");
            
            DapperPlusManager.Entity<PatientVisitExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(PatientVisitExtract)}");

            DapperPlusManager.Entity<PatientPharmacyExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(PatientPharmacyExtract)}");

            DapperPlusManager.Entity<PatientLaboratoryExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(PatientLaboratoryExtract)}");

            DapperPlusManager.Entity<PatientArtExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(PatientArtExtract)}");

            DapperPlusManager.Entity<AllergiesChronicIllnessExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(AllergiesChronicIllnessExtract)}");

            DapperPlusManager.Entity<ContactListingExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(ContactListingExtract)}");

            DapperPlusManager.Entity<CovidExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(CovidExtract)}");

            DapperPlusManager.Entity<DefaulterTracingExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(DefaulterTracingExtract)}");

            DapperPlusManager.Entity<DepressionScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(DepressionScreeningExtract)}");

            DapperPlusManager.Entity<DrugAlcoholScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(DrugAlcoholScreeningExtract)}");

            DapperPlusManager.Entity<EnhancedAdherenceCounsellingExtract>()
               .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
               .Table($"{nameof(EnhancedAdherenceCounsellingExtract)}");

            DapperPlusManager.Entity<GbvScreeningExtract>()
               .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
               .Table($"{nameof(GbvScreeningExtract)}");

            DapperPlusManager.Entity<IptExtract>()
               .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
               .Table($"{nameof(IptExtract)}");

            DapperPlusManager.Entity<OvcExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(OvcExtract)}");

            DapperPlusManager.Entity<OtzExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(OtzExtract)}");

            DapperPlusManager.Entity<PatientAdverseEventExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(PatientAdverseEventExtract)}");

            DapperPlusManager.Entity<PatientBaselinesExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(PatientBaselinesExtract)}");

            DapperPlusManager.Entity<PatientStatusExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(PatientStatusExtract)}");

            DapperPlusManager.Entity<CervicalCancerScreeningExtract>()
              .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(CervicalCancerScreeningExtract)}");

            DapperPlusManager.Entity<IITRiskScore>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
               .Table($"{nameof(IITRiskScoresExtract)}");


            DapperPlusManager.Entity<StagePatientExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(StagePatientExtracts)}");

            DapperPlusManager.Entity<StageVisitExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageVisitExtracts)}");

            DapperPlusManager.Entity<StageAdverseEventExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageAdverseEventExtracts)}");

            DapperPlusManager.Entity<StageAllergiesChronicIllnessExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageAllergiesChronicIllnessExtracts)}");

            DapperPlusManager.Entity<StageArtExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageArtExtracts)}");

            DapperPlusManager.Entity<StageBaselineExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageBaselineExtracts)}");

            DapperPlusManager.Entity<StageContactListingExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageContactListingExtracts)}");

            DapperPlusManager.Entity<StageCovidExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageCovidExtracts)}");

            DapperPlusManager.Entity<StageDefaulterTracingExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageDefaulterTracingExtracts)}");

            DapperPlusManager.Entity<StageDepressionScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageDepressionScreeningExtracts)}");

            DapperPlusManager.Entity<StageDrugAlcoholScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageDrugAlcoholScreeningExtracts)}");

            DapperPlusManager.Entity<StageEnhancedAdherenceCounsellingExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageEnhancedAdherenceCounsellingExtracts)}");

            DapperPlusManager.Entity<StageIptExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageIptExtracts)}");

            DapperPlusManager.Entity<StageLaboratoryExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageLaboratoryExtracts)}");

            DapperPlusManager.Entity<StageOtzExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageOtzExtracts)}");

            DapperPlusManager.Entity<StageOvcExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageOvcExtracts)}");

            DapperPlusManager.Entity<StagePharmacyExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StagePharmacyExtracts)}");

            DapperPlusManager.Entity<StageStatusExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageStatusExtracts)}");

            DapperPlusManager.Entity<StageGbvScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageGbvScreeningExtracts)}");

            DapperPlusManager.Entity<StageCervicalCancerScreeningExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                .Table($"{nameof(StageCervicalCancerScreeningExtracts)}");

            DapperPlusManager.Entity<StageIITRiskScore>().Key(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
              .Table($"{nameof(StageIITRiskScoresExtracts)}");
        }

        

        public virtual void EnsureSeeded()
        {
            // for seeding 
            if (!MasterFacilities.Any())
            {
                MasterFacilities.AddRange(new List<MasterFacility>
                {
                    new MasterFacility(-10000,"Demo","Demo")
                });
            }
            
            if (!Facilities.Any())
            {
                Facilities.AddRange(new List<Facility>
                {
                    new Facility(-10000,"Demo")
                });
            }

            if (!PatientExtract.Any())
            {
                PatientExtract.AddRange(new List<PatientExtract>
                {
                    new PatientExtract() { PatientPk = 1, SiteCode = -10000, CccNumber = "C01" ,Gender="F"},
                    new PatientExtract() { PatientPk = 2, SiteCode = -10000, CccNumber = "C02" ,Gender="M" }
                });
            }
            
            if (!PatientVisitExtract.Any())
            {
                PatientVisitExtract.AddRange(new List<PatientVisitExtract>
                {
                    new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=001,VisitDate=DateTime.Today.AddDays(-9)},
                    new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=002,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!PatientPharmacyExtract.Any())
            {
                PatientPharmacyExtract.AddRange(new List<PatientPharmacyExtract>
                {
                    new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=001,DispenseDate=DateTime.Today.AddDays(-9)},
                    new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=002,DispenseDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!PatientLaboratoryExtract.Any())
            {
                PatientLaboratoryExtract.AddRange(new List<PatientLaboratoryExtract>
                {
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=101,OrderedByDate=DateTime.Today.AddDays(-9)},
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=102,OrderedByDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!PatientArtExtract.Any())
            {
                PatientArtExtract.AddRange(new List<PatientArtExtract>
                {
                    new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,LastARTDate=DateTime.Today.AddDays(-9)},
                    new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,LastARTDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!AllergiesChronicIllnessExtract.Any())
            {
                AllergiesChronicIllnessExtract.AddRange(new List<AllergiesChronicIllnessExtract>
                {
                    new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!ContactListingExtract.Any())
            {
                ContactListingExtract.AddRange(new List<ContactListingExtract>
                {
                    new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!CovidExtract.Any())
            {
                CovidExtract.AddRange(new List<CovidExtract>
                {
                    new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,Covid19AssessmentDate=DateTime.Today.AddDays(-9)},
                    new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,Covid19AssessmentDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DefaulterTracingExtract.Any())
            {
                DefaulterTracingExtract.AddRange(new List<DefaulterTracingExtract>
                {
                    new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DepressionScreeningExtract.Any())
            {
                DepressionScreeningExtract.AddRange(new List<DepressionScreeningExtract>
                {
                    new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DrugAlcoholScreeningExtract.Any())
            {
                DrugAlcoholScreeningExtract.AddRange(new List<DrugAlcoholScreeningExtract>
                {
                    new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!EnhancedAdherenceCounsellingExtract.Any())
            {
                EnhancedAdherenceCounsellingExtract.AddRange(new List<EnhancedAdherenceCounsellingExtract>
                {
                    new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!GbvScreeningExtract.Any())
            {
                GbvScreeningExtract.AddRange(new List<GbvScreeningExtract>
                {
                    new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!IptExtract.Any())
            {
                IptExtract.AddRange(new List<IptExtract>
                {
                    new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!OvcExtract.Any())
            {
                OvcExtract.AddRange(new List<OvcExtract>
                {
                    new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!OtzExtract.Any())
            {
                OtzExtract.AddRange(new List<OtzExtract>
                {
                    new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!PatientAdverseEventExtract.Any())
            {
                PatientAdverseEventExtract.AddRange(new List<PatientAdverseEventExtract>
                {
                    new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!PatientBaselinesExtract.Any())
            {
                PatientBaselinesExtract.AddRange(new List<PatientBaselinesExtract>
                {
                    new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!PatientStatusExtract.Any())
            {
                PatientStatusExtract.AddRange(new List<PatientStatusExtract>
                {
                    new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000 }
                });
            }

            SaveChanges();
        }
    }
}
