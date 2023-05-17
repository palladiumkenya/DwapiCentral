using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Context
{
    public class CtDbContext : DbContext
    {
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Metric> Metrics { get; set; }

        public DbSet<PatientExtract> PatientExtracts { get; set; }  
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
        public DbSet<PatientArtExtract> PatientArtExtracts { get; set; }
        public DbSet<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }
        public DbSet<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }
        public DbSet<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }
        public DbSet<PatientStatusExtract> PatientStatusExtracts { get; set; }
        public DbSet<PatientVisitExtract> PatientVisitExtracts { get; set; }
  


        public CtDbContext(DbContextOptions<CtDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientExtract>()
            .HasIndex(p => new { p.PatientPID, p.SiteCode, p.FacilityId })
            .IsUnique(true);

            modelBuilder.Entity<PatientLaboratoryExtract>()
                .HasIndex(p => new {p.Id })
                .IsUnique(true);

            modelBuilder.Entity<PatientVisitExtract>()
                .HasIndex(p => new { p.VisitId, p.VisitDate, p.PatientId })
                .IsUnique(true);
                


            base.OnModelCreating(modelBuilder);

        }
    }
}
