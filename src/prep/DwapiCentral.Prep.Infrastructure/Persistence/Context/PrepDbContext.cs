using CsvHelper;
using CsvHelper.Configuration;
using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Prep.Domain.Models;
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



        //Stage





        public PrepDbContext(DbContextOptions<PrepDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);





            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Code).Table($"{nameof(MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Code).Table($"{nameof(Facilities)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(Cargoes)}");
            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(Subscribers)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(Manifests)}");


            //stage

        }

        public virtual void EnsureSeeded()
        {
            SeedFromCsv<Docket>("DwapiCentral.Mnch.Infrastructure.Persistence.Seed.Docket.csv");
            SeedFromCsv<Subscriber>("DwapiCentral.Mnch.Infrastructure.Persistence.Seed.Subscriber.csv");


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
