using CsvHelper;
using CsvHelper.Configuration;
using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Model.Stage;
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

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Context
{
    public class MnchDbContext : DbContext
    {
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }

        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<PatientMnchExtract> MnchPatients { get; set; }
        public DbSet<MnchEnrolment> MnchEnrolments { get; set; }
        public DbSet<MnchArt> MnchArts { get; set; }
        public DbSet<AncVisit> AncVisits { get; set; }
        public DbSet<MatVisit> MatVisits { get; set; }
        public DbSet<PncVisit> PncVisits { get; set; }
        public DbSet<MotherBabyPair> MotherBabyPairs { get; set; }
        public DbSet<CwcEnrolment> CwcEnrolments { get; set; }
        public DbSet<CwcVisit> CwcVisits { get; set; }
        public DbSet<HeiExtract> Heis { get; set; }
        public DbSet<MnchLab> MnchLabs { get; set; }
        public DbSet<MnchImmunization> MnchImmunizations { get; set; }

        //Stage
        public DbSet<StagePatientMnchExtract> StageMnchPatients { get; set; }
        public DbSet<StageMnchEnrolment> StageMnchEnrolments { get; set; }
        public DbSet<StageMnchArt> StageMnchArts { get; set; }
        public DbSet<StageAncVisit> StageAncVisits { get; set; }
        public DbSet<StageMatVisit> StageMatVisits { get; set; }
        public DbSet<StagePncVisit> StagePncVisits { get; set; }
        public DbSet<StageMotherBabyPair> StageMotherBabyPairs { get; set; }
        public DbSet<StageCwcEnrolment> StageCwcEnrolments { get; set; }
        public DbSet<StageCwcVisit> StageCwcVisits { get; set; }
        public DbSet<StageHeiExtract> StageHeis { get; set; }
        public DbSet<StageMnchLab> StageMnchLabs { get; set; }
        public DbSet<StageMnchImmunization> StageMnchImmunizations { get; set; }




        public MnchDbContext(DbContextOptions<MnchDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientMnchExtract>()
                .HasKey(m => new { m.PatientPk, m.SiteCode });

            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MnchEnrolmentExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MnchArtExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.AncVisitExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MatVisitExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.PncVisitExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MotherBabyPairExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.CwcEnrolmentExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.CwcVisitExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.HeiExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MnchLabExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();


            modelBuilder.Entity<PatientMnchExtract>()
                .HasMany(c => c.MnchImmunizationExtracts)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode })
                .IsRequired();

            modelBuilder.Entity<StagePatientMnchExtract>()
              .HasKey(m => new { m.PatientPk, m.SiteCode });




            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(Cargoes)}");
            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(Subscribers)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(Manifests)}");

            DapperPlusManager.Entity<PatientMnchExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(MnchPatients)}");


            DapperPlusManager.Entity<StagePatientMnchExtract>()
                .Key(x => new { x.PatientPk, x.SiteCode })
                .Table($"{nameof(StageMnchPatients)}");

            DapperPlusManager.Entity<MnchEnrolment>().Key(x => x.Id).Table($"{nameof(MnchEnrolments)}");
            DapperPlusManager.Entity<MnchArt>().Key(x => x.Id).Table($"{nameof(MnchArts)}");
            DapperPlusManager.Entity<AncVisit>().Key(x => x.Id).Table($"{nameof(AncVisits)}");
            DapperPlusManager.Entity<MatVisit>().Key(x => x.Id).Table($"{nameof(MatVisits)}");
            DapperPlusManager.Entity<PncVisit>().Key(x => x.Id).Table($"{nameof(PncVisits)}");
            DapperPlusManager.Entity<MotherBabyPair>().Key(x => x.Id).Table($"{nameof(MotherBabyPairs)}");
            DapperPlusManager.Entity<CwcEnrolment>().Key(x => x.Id).Table($"{nameof(CwcEnrolments)}");
            DapperPlusManager.Entity<CwcVisit>().Key(x => x.Id).Table($"{nameof(CwcVisits)}");
            DapperPlusManager.Entity<HeiExtract>().Key(x => x.Id).Table($"{nameof(Heis)}");
            DapperPlusManager.Entity<MnchLab>().Key(x => x.Id).Table($"{nameof(MnchLabs)}");
            DapperPlusManager.Entity<MnchImmunization>().Key(x => x.Id).Table($"{nameof(MnchImmunizations)}");

            //stage
            DapperPlusManager.Entity<StageMnchEnrolment>().Key(x => x.Id).Table($"{nameof(StageMnchEnrolments)}");
            DapperPlusManager.Entity<StageMnchArt>().Key(x => x.Id).Table($"{nameof(StageMnchArts)}");
            DapperPlusManager.Entity<StageAncVisit>().Key(x => x.Id).Table($"{nameof(StageAncVisits)}");
            DapperPlusManager.Entity<StageMatVisit>().Key(x => x.Id).Table($"{nameof(StageMatVisits)}");
            DapperPlusManager.Entity<StagePncVisit>().Key(x => x.Id).Table($"{nameof(StagePncVisits)}");
            DapperPlusManager.Entity<StageMotherBabyPair>().Key(x => x.Id).Table($"{nameof(StageMotherBabyPairs)}");
            DapperPlusManager.Entity<StageCwcEnrolment>().Key(x => x.Id).Table($"{nameof(StageCwcEnrolments)}");
            DapperPlusManager.Entity<StageCwcVisit>().Key(x => x.Id).Table($"{nameof(StageCwcVisits)}");
            DapperPlusManager.Entity<StageHeiExtract>().Key(x => x.Id).Table($"{nameof(StageHeis)}");
            DapperPlusManager.Entity<StageMnchLab>().Key(x => x.Id).Table($"{nameof(StageMnchLabs)}");
            DapperPlusManager.Entity<StageMnchImmunization>().Key(x => x.Id).Table($"{nameof(StageMnchImmunizations)}");



        }

        public virtual void EnsureSeeded()
        {
            SeedFromCsv<Docket>("DwapiCentral.Mnch.Infrastructure.Persistence.Seed.Docket.csv");
            SeedFromCsv<Subscriber>("DwapiCentral.Mnch.Infrastructure.Persistence.Seed.Subscriber.csv");
            //SeedFromCsv<MasterFacility>("DwapiCentral.Mnch.Infrastructure.Persistence.Seed.MasterFacility.csv");

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
