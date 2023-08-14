using CsvHelper;
using CsvHelper.Configuration;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
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

namespace DwapiCentral.Hts.Infrastructure.Persistence.Context
{
    public class HtsDbContext : DbContext
    {
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }

        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<HtsClient> Clients { get; set; }
        public DbSet<HtsClientLinkage> ClientLinkages { get; set; }
        public DbSet<HtsClientPartner> ClientPartners { get; set; }
        public DbSet<HtsClientTest> HtsClientTests { get; set; }
        public DbSet<HtsClientTracing> HtsClientTracing { get; set; }
        public DbSet<HtsPartnerNotificationServices> HtsPartnerNotificationServices { get; set; }
        public DbSet<HtsPartnerTracing> HtsPartnerTracings { get; set; }
        public DbSet<HtsTestKit> HtsTestKits { get; set;}

        //Stage
        public virtual DbSet<StageHtsClient> StageClients { get; set; }
        public virtual DbSet<StageHtsClientLinkage> StageClientLinkages { get; set; }
        public virtual DbSet<StageHtsClientPartner> StageClientPartners { get; set; }
        public virtual DbSet<StageHtsClientTest> StageHtsClientTests { get; set; }
        public virtual DbSet<StageHtsClientTracing> StageHtsClientTracing { get; set; }
        public virtual DbSet<StageHtsPartnerNotificationServices> StageHtsPartnerNotificationServices { get; set; }
        public virtual DbSet<StageHtsPartnerTracing> StageHtsPartnerTracings { get; set; }
        public virtual DbSet<StageHtsTestKit> StageHtsTestKits { get; set; }


        public HtsDbContext(DbContextOptions<HtsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HtsClient>()
                 .HasKey(m => new { m.PatientPk, m.SiteCode,m.HtsNumber });

            modelBuilder.Entity<HtsClient>()
                .HasMany(c => c.HtsClientLinkages)
                .WithOne()
                .HasForeignKey(f => new { f.PatientPk, f.SiteCode,f.HtsNumber })
                .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsClientPartners)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsClientTests)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsClientTracings)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsPartnerNotificationServices)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsPartnerTracings)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

            modelBuilder.Entity<HtsClient>()
               .HasMany(c => c.HtsTestKits)
               .WithOne()
               .HasForeignKey(f => new { f.PatientPk, f.SiteCode, f.HtsNumber })
               .IsRequired();

           
            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");

            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");

            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(Cargoes)}");

            DapperPlusManager.Entity<HtsClient>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.HtsNumber })
                .Table($"{nameof(Clients)}");
            DapperPlusManager.Entity<StageHtsClient>()
                .Key(x => new { x.PatientPk, x.SiteCode, x.HtsNumber })
                .Table($"{nameof(StageClients)}");


            DapperPlusManager.Entity<HtsClientLinkage>()
                .Key(x => new { x.Id })
                .Table($"{nameof(ClientLinkages)}");
            DapperPlusManager.Entity<StageHtsClientLinkage>()
                .Key(x => new { x.Id })
                .Table($"{nameof(Clients)}");

            DapperPlusManager.Entity<HtsClientPartner>()
               .Key(x => new { x.Id })
               .Table($"{nameof(ClientPartners)}");
            DapperPlusManager.Entity<StageHtsClientPartner>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageClientPartners)}");

            DapperPlusManager.Entity<HtsClientTest>()
               .Key(x => new { x.Id })
               .Table($"{nameof(HtsClientTests)}");
            DapperPlusManager.Entity<StageHtsClientTest>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageHtsClientTests)}");

            DapperPlusManager.Entity<HtsClientTracing>()
               .Key(x => new { x.Id })
               .Table($"{nameof(HtsClientTracing)}");
            DapperPlusManager.Entity<StageHtsClientTracing>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageHtsClientTracing)}");

            DapperPlusManager.Entity<HtsPartnerNotificationServices>()
               .Key(x => new { x.Id })
               .Table($"{nameof(HtsPartnerNotificationServices)}");
            DapperPlusManager.Entity<StageHtsPartnerNotificationServices>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageHtsPartnerNotificationServices)}");

            DapperPlusManager.Entity<HtsPartnerTracing>()
               .Key(x => new { x.Id })
               .Table($"{nameof(HtsPartnerTracings)}");
            DapperPlusManager.Entity<StageHtsPartnerTracing>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageHtsPartnerTracings)}");

            DapperPlusManager.Entity<HtsTestKit>()
               .Key(x => new { x.Id })
               .Table($"{nameof(HtsTestKits)}");
            DapperPlusManager.Entity<StageHtsTestKit>()
                .Key(x => new { x.Id })
                .Table($"{nameof(StageHtsTestKits)}");

        }

        public virtual void EnsureSeeded()
        {
            SeedFromCsv<Docket>("DwapiCentral.Hts.Infrastructure.Persistence.Seed.Docket.csv");
            SeedFromCsv<Subscriber>("DwapiCentral.Hts.Infrastructure.Persistence.Seed.Subscriber.csv");
          
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
