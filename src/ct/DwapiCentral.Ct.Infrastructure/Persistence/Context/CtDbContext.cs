using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

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
        // public DbSet<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; }
        // public DbSet<ContactListingExtract> contactListingExtracts { get; set; }
        // public DbSet<CovidExtract> CovidExtracts { get; set; }
        // public DbSet<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; }
        // public DbSet<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; }
        // public DbSet<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; } 
        // public DbSet<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; }
        // public DbSet<GbvScreeningExtract> GbvScreeningExtracts { get; set; }
        // public DbSet<IptExtract> IptExtracts { get; set; }
        // public DbSet<OvcExtract> OvcExtracts { get; set; }
        // public DbSet<OtzExtract> OtzExtracts { get; set; }
        // public DbSet<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; }  

        // public DbSet<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }


        // public DbSet<PatientStatusExtract> PatientStatusExtracts { get; set; }

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

            modelBuilder.Entity<PatientExtract>()
              .HasMany(c => c.PatientPharmacyExtracts)
              .WithOne()
              .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
              .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientLaboratoryExtracts)
                .WithOne()
                .HasForeignKey(p => new { p.PatientId, p.SiteCode })
                .IsRequired();

            modelBuilder.Entity<PatientExtract>()
                .HasMany(p => p.PatientArtExtracts)
                .WithOne()
                .HasForeignKey(p => new {p.PatientId,p.SiteCode })
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
                .Table($"{nameof(PatientPharmacyExtract)}");

            DapperPlusManager.Entity<PatientLaboratoryExtract>()
                .Key(x => x.Id)
                .Table($"{nameof(PatientLaboratoryExtract)}");

            DapperPlusManager.Entity<PatientArtExtract>()
                .Key(x => x.Id)
                .Table($"{typeof(PatientArtExtract)}");
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

            if (!PatientLaboratoryExtracts.Any())
            {
                PatientLaboratoryExtracts.AddRange(new List<PatientLaboratoryExtract>
                {
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE1-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=101,OrderedByDate=DateTime.Today.AddDays(-9)},
                    new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AAE2-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=102,OrderedByDate=DateTime.Today.AddDays(-8)}
                });
            }

            SaveChanges();
        }
    }
}
