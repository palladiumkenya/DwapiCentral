using CsvHelper;
using CsvHelper.Configuration;
using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Models.Stage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Context
{
    public class PrepDbContext : DbContext
    {
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }

        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<PatientPrepExtract> PrepPatients { get; set; }
        public DbSet<PrepAdverseEvent> PrepAdverseEvents { get; set; }
        public DbSet<PrepBehaviourRisk> PrepBehaviourRisks { get; set; }
        public DbSet<PrepCareTermination> PrepCareTerminations { get; set; }
        public DbSet<PrepLab> PrepLabs { get; set; }
        public DbSet<PrepPharmacy> PrepPharmacys { get; set; }
        public DbSet<PrepVisit> PrepVisits { get; set; }
        public DbSet<PrepMonthlyRefill> PrepMonthlyRefills { get; set; }

        //Stage
        public DbSet<StagePatientPrep> StagePrepPatients { get; set; }
        public DbSet<StagePrepAdverseEvent> StagePrepAdverseEvents { get; set; }
        public DbSet<StagePrepBehaviourRisk> StagePrepBehaviourRisks { get; set; }
        public DbSet<StagePrepCareTermination> StagePrepCareTerminations { get; set; }
        public DbSet<StagePrepLab> StagePrepLabs { get; set; }
        public DbSet<StagePrepPharmacy> StagePrepPharmacys { get; set; }
        public DbSet<StagePrepVisit> StagePrepVisits { get; set; }
        public DbSet<StagePrepMonthlyRefill> StagePrepMonthlyRefills { get; set; }



        public PrepDbContext(DbContextOptions<PrepDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientPrepExtract>()
                .HasKey(m => new { m.PatientPk, m.SiteCode, m.PrepNumber });

            modelBuilder.Entity<StagePatientPrep>()
                .HasKey(m => new { m.PatientPk, m.SiteCode, m.PrepNumber });

            modelBuilder.Entity<PatientPrepExtract>()
                .HasMany(c => c.PrepAdverseEvents)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode ,f.PrepNumber})
                .IsRequired();


            modelBuilder.Entity<PatientPrepExtract>()
                .HasMany(c => c.PrepBehaviourRisks)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode,f.PrepNumber })
                .IsRequired();

            modelBuilder.Entity<PatientPrepExtract>()
               .HasMany(c => c.PrepCareTerminations)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.PrepNumber })
               .IsRequired();

            modelBuilder.Entity<PatientPrepExtract>()
               .HasMany(c => c.PrepLabs)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.PrepNumber })
               .IsRequired();

            modelBuilder.Entity<PatientPrepExtract>()
               .HasMany(c => c.PrepPharmacies)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.PrepNumber })
               .IsRequired();

            modelBuilder.Entity<PatientPrepExtract>()
               .HasMany(c => c.PrepVisits)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.PrepNumber })
               .IsRequired();

            modelBuilder.Entity<PatientPrepExtract>()
           .HasMany(c => c.PrepMonthlyRefills)
           .WithOne()
           .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.PrepNumber })
           .IsRequired();



            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(Cargoes)}");
            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(Subscribers)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(Manifests)}");

            DapperPlusManager.Entity<PatientPrepExtract>()
               .Key(x => new { x.PatientPk, x.SiteCode ,x.PrepNumber,x.RecordUUID})
               .Table($"{nameof(PrepPatients)}");

            DapperPlusManager.Entity<StagePatientPrep>()
               .Key(x => new { x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID })
                .Table($"{nameof(StagePrepPatients)}");

            DapperPlusManager.Entity<PrepAdverseEvent>().Key(x => x.RecordUUID).Table($"{nameof(PrepAdverseEvents)}");
            DapperPlusManager.Entity<PrepBehaviourRisk>().Key(x => x.RecordUUID).Table($"{nameof(PrepBehaviourRisks)}");
            DapperPlusManager.Entity<PrepCareTermination>().Key(x => x.RecordUUID).Table($"{nameof(PrepCareTerminations)}");
            DapperPlusManager.Entity<PrepLab>().Key(x => x.RecordUUID).Table($"{nameof(PrepLabs)}");
            DapperPlusManager.Entity<PrepPharmacy>().Key(x => x.RecordUUID).Table($"{nameof(PrepPharmacys)}");
            DapperPlusManager.Entity<PrepVisit>().Key(x => x.RecordUUID).Table($"{nameof(PrepVisits)}");
            DapperPlusManager.Entity<PrepMonthlyRefill>().Key(x => x.RecordUUID).Table($"{nameof(PrepMonthlyRefills)}");

            //stage
            DapperPlusManager.Entity<StagePrepAdverseEvent>().Key(x => x.Id).Table($"{nameof(StagePrepAdverseEvents)}");
            DapperPlusManager.Entity<StagePrepBehaviourRisk>().Key(x => x.Id).Table($"{nameof(StagePrepBehaviourRisks)}");
            DapperPlusManager.Entity<StagePrepCareTermination>().Key(x => x.Id).Table($"{nameof(StagePrepCareTerminations)}");
            DapperPlusManager.Entity<StagePrepLab>().Key(x => x.Id).Table($"{nameof(StagePrepLabs)}");
            DapperPlusManager.Entity<StagePrepPharmacy>().Key(x => x.Id).Table($"{nameof(StagePrepPharmacys)}");
            DapperPlusManager.Entity<StagePrepVisit>().Key(x => x.Id).Table($"{nameof(StagePrepVisits)}");
            DapperPlusManager.Entity<StagePrepMonthlyRefill>().Key(x => x.Id).Table($"{nameof(StagePrepMonthlyRefills)}");

        }

        public virtual void EnsureSeeded()
        {
            SeedFromCsv<Docket>("DwapiCentral.Prep.Infrastructure.Persistence.Seed.Docket.csv");
            SeedFromCsv<Subscriber>("DwapiCentral.Prep.Infrastructure.Persistence.Seed.Subscriber.csv");
            //SeedFromCsv<MasterFacility>("DwapiCentral.Prep.Infrastructure.Persistence.Seed.MasterFacility.csv");

            SaveChanges();



        }

        private void SeedFromCsv<T>(string resourceName) where T : class
        {
            using var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "|",
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csv.GetRecords<T>().ToList();
            var dbSet = Set<T>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(records);
            }
        }


    }
}
