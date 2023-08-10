using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
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
        public DbSet<PatientExtract> PatientExtracts { get; set; }  
        public DbSet<PatientVisitExtract> PatientVisitExtracts { get; set; }
        public DbSet<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }
        public DbSet<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }
        public DbSet<PatientArtExtract> PatientArtExtracts { get; set; }
        public DbSet<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; }
        public DbSet<ContactListingExtract> contactListingExtracts { get; set; }
        public DbSet<CovidExtract> CovidExtracts { get; set; }
        public DbSet<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; }
        public DbSet<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; }
        public DbSet<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; } 
        public DbSet<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; }
        public DbSet<GbvScreeningExtract> GbvScreeningExtracts { get; set; }
        public DbSet<IptExtract> IptExtracts { get; set; }
        public DbSet<OvcExtract> OvcExtracts { get; set; }
        public DbSet<OtzExtract> OtzExtracts { get; set; }
        public DbSet<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; } 
        public DbSet<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }
        public DbSet<PatientStatusExtract> PatientStatusExtracts { get; set; }
        public DbSet<CervicalCancerScreeningExtract> CervicalCancerScreeningExtracts { get; set; }

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
        //public virtual DbSet<SmartActionRegister> SmartActionRegisters { get; set; }

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

            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");

            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");
            
            DapperPlusManager.Entity<Metric>().Key(x => x.Id).Table($"{nameof(Metrics)}");
            
            DapperPlusManager.Entity<PatientExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(PatientExtracts)}");
            
            DapperPlusManager.Entity<PatientVisitExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(PatientVisitExtracts)}");

            DapperPlusManager.Entity<PatientPharmacyExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(PatientPharmacyExtracts)}");

            DapperPlusManager.Entity<PatientLaboratoryExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(PatientLaboratoryExtracts)}");

            DapperPlusManager.Entity<PatientArtExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(PatientArtExtracts)}");

            DapperPlusManager.Entity<AllergiesChronicIllnessExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(AllergiesChronicIllnessExtracts)}");

            DapperPlusManager.Entity<ContactListingExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(contactListingExtracts)}");

            DapperPlusManager.Entity<CovidExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(CovidExtracts)}");

            DapperPlusManager.Entity<DefaulterTracingExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(DefaulterTracingExtracts)}");

            DapperPlusManager.Entity<DepressionScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(DepressionScreeningExtracts)}");

            DapperPlusManager.Entity<DrugAlcoholScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(DrugAlcoholScreeningExtracts)}");

            DapperPlusManager.Entity<EnhancedAdherenceCounsellingExtract>()
               .Key(x => x.Id)
               .Table($"{nameof(EnhancedAdherenceCounsellingExtracts)}");

            DapperPlusManager.Entity<GbvScreeningExtract>()
               .Key(x => x.Id)
               .Table($"{nameof(GbvScreeningExtracts)}");

            DapperPlusManager.Entity<IptExtract>()
               .Key(x => x.Id)
               .Table($"{nameof(IptExtracts)}");

            DapperPlusManager.Entity<OvcExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(OvcExtracts)}");

            DapperPlusManager.Entity<OtzExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(OtzExtracts)}");

            DapperPlusManager.Entity<PatientAdverseEventExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(PatientAdverseEventExtracts)}");

            DapperPlusManager.Entity<PatientBaselinesExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(PatientBaselinesExtracts)}");

            DapperPlusManager.Entity<PatientStatusExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(PatientStatusExtracts)}");

            DapperPlusManager.Entity<CervicalCancerScreeningExtract>()
              .Key(x => x.Id)
              .Table($"{nameof(CervicalCancerScreeningExtracts)}");

            DapperPlusManager.Entity<StagePatientExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(StagePatientExtracts)}");

            DapperPlusManager.Entity<StageVisitExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageVisitExtracts)}");

            DapperPlusManager.Entity<StageAdverseEventExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageAdverseEventExtracts)}");

            DapperPlusManager.Entity<StageAllergiesChronicIllnessExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageAllergiesChronicIllnessExtracts)}");

            DapperPlusManager.Entity<StageArtExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageArtExtracts)}");

            DapperPlusManager.Entity<StageBaselineExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageBaselineExtracts)}");

            DapperPlusManager.Entity<StageContactListingExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageContactListingExtracts)}");

            DapperPlusManager.Entity<StageCovidExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageCovidExtracts)}");

            DapperPlusManager.Entity<StageDefaulterTracingExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageDefaulterTracingExtracts)}");

            DapperPlusManager.Entity<StageDepressionScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageDepressionScreeningExtracts)}");

            DapperPlusManager.Entity<StageDrugAlcoholScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageDrugAlcoholScreeningExtracts)}");

            DapperPlusManager.Entity<StageEnhancedAdherenceCounsellingExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageEnhancedAdherenceCounsellingExtracts)}");

            DapperPlusManager.Entity<StageIptExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageIptExtracts)}");

            DapperPlusManager.Entity<StageLaboratoryExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageLaboratoryExtracts)}");

            DapperPlusManager.Entity<StageOtzExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageOtzExtracts)}");

            DapperPlusManager.Entity<StageOvcExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageOvcExtracts)}");

            DapperPlusManager.Entity<StagePharmacyExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StagePharmacyExtracts)}");

            DapperPlusManager.Entity<StageStatusExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageStatusExtracts)}");

            DapperPlusManager.Entity<StageGbvScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageGbvScreeningExtracts)}");

            DapperPlusManager.Entity<StageCervicalCancerScreeningExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(StageCervicalCancerScreeningExtracts)}");
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

            if (!PatientExtracts.Any())
            {
                PatientExtracts.AddRange(new List<PatientExtract>
                {
                    new PatientExtract() { PatientPk = 1, SiteCode = -10000, CccNumber = "C01" ,Gender="F"},
                    new PatientExtract() { PatientPk = 2, SiteCode = -10000, CccNumber = "C02" ,Gender="M" }
                });
            }
            
            if (!PatientVisitExtracts.Any())
            {
                PatientVisitExtracts.AddRange(new List<PatientVisitExtract>
                {
                    new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=001,VisitDate=DateTime.Today.AddDays(-9)},
                    new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=002,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!PatientPharmacyExtracts.Any())
            {
                PatientPharmacyExtracts.AddRange(new List<PatientPharmacyExtract>
                {
                    new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=001,DispenseDate=DateTime.Today.AddDays(-9)},
                    new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=002,DispenseDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!PatientLaboratoryExtracts.Any())
            {
                PatientLaboratoryExtracts.AddRange(new List<PatientLaboratoryExtract>
                {
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=101,OrderedByDate=DateTime.Today.AddDays(-9)},
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=102,OrderedByDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!PatientArtExtracts.Any())
            {
                PatientArtExtracts.AddRange(new List<PatientArtExtract>
                {
                    new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,LastARTDate=DateTime.Today.AddDays(-9)},
                    new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,LastARTDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!AllergiesChronicIllnessExtracts.Any())
            {
                AllergiesChronicIllnessExtracts.AddRange(new List<AllergiesChronicIllnessExtract>
                {
                    new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!contactListingExtracts.Any())
            {
                contactListingExtracts.AddRange(new List<ContactListingExtract>
                {
                    new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!CovidExtracts.Any())
            {
                CovidExtracts.AddRange(new List<CovidExtract>
                {
                    new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,Covid19AssessmentDate=DateTime.Today.AddDays(-9)},
                    new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,Covid19AssessmentDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DefaulterTracingExtracts.Any())
            {
                DefaulterTracingExtracts.AddRange(new List<DefaulterTracingExtract>
                {
                    new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DepressionScreeningExtracts.Any())
            {
                DepressionScreeningExtracts.AddRange(new List<DepressionScreeningExtract>
                {
                    new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!DrugAlcoholScreeningExtracts.Any())
            {
                DrugAlcoholScreeningExtracts.AddRange(new List<DrugAlcoholScreeningExtract>
                {
                    new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }
            if (!EnhancedAdherenceCounsellingExtracts.Any())
            {
                EnhancedAdherenceCounsellingExtracts.AddRange(new List<EnhancedAdherenceCounsellingExtract>
                {
                    new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!GbvScreeningExtracts.Any())
            {
                GbvScreeningExtracts.AddRange(new List<GbvScreeningExtract>
                {
                    new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!IptExtracts.Any())
            {
                IptExtracts.AddRange(new List<IptExtract>
                {
                    new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!OvcExtracts.Any())
            {
                OvcExtracts.AddRange(new List<OvcExtract>
                {
                    new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!OtzExtracts.Any())
            {
                OtzExtracts.AddRange(new List<OtzExtract>
                {
                    new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-9)},
                    new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=102,VisitDate=DateTime.Today.AddDays(-8)}
                });
            }

            if (!PatientAdverseEventExtracts.Any())
            {
                PatientAdverseEventExtracts.AddRange(new List<PatientAdverseEventExtract>
                {
                    new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!PatientBaselinesExtracts.Any())
            {
                PatientBaselinesExtracts.AddRange(new List<PatientBaselinesExtract>
                {
                    new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
                });
            }

            if (!PatientStatusExtracts.Any())
            {
                PatientStatusExtracts.AddRange(new List<PatientStatusExtract>
                {
                    new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
                    new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000 }
                });
            }

            SaveChanges();
        }
    }
}
