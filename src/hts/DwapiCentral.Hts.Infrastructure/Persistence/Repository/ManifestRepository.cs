using Dapper;
using DwapiCentral.Contracts.Hts;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Infrastructure.Persistence.Repository
{
    public class ManifestRepository : IManifestRepository
    {
        private readonly HtsDbContext _context;

        public ManifestRepository(HtsDbContext context)
        {
            _context = context;
        }

        public async Task ClearFacility(int siteCode)
        {
            var cons = _context.Database.GetDbConnection();

            var sql = @"

        delete  from StageClients WHERE  SiteCode = @SiteCode;
        delete  from StageClientLinkages WHERE  SiteCode = @SiteCode;
        delete  from StageClientPartners WHERE  SiteCode = @SiteCode;
        delete  from StageHtsClientTests WHERE  SiteCode = @SiteCode;
        delete  from StageHtsClientTracing WHERE  SiteCode = @SiteCode;
        delete  from StageHtsPartnerNotificationServices WHERE  SiteCode = @SiteCode;
        delete  from StageHtsPartnerTracings WHERE  SiteCode = @SiteCode;
        delete  from StageHtsTestKits WHERE  SiteCode = @SiteCode;
        

        ";
            try
            {

                if (cons.State != ConnectionState.Open)
                    cons.Open();

                using (var transaction = cons.BeginTransaction())
                {
                    await cons.ExecuteAsync($"{sql}", new { siteCode }, transaction, 0);
                    transaction.Commit();
                }

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        public async Task ClearFacility(int siteCode, string project)
        {
            var cons = _context.Database.GetDbConnection();

            var sql = @"

        delete  from StageClients WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageClientLinkages WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageClientPartners WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHtsClientTests WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHtsClientTracing WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHtsPartnerNotificationServices WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHtsPartnerTracings WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHtsTestKits WHERE  SiteCode = @SiteCode AND Project = @project;
        

        ";
            try
            {

                if (cons.State != ConnectionState.Open)
                    cons.Open();

                using (var transaction = cons.BeginTransaction())
                {
                    await cons.ExecuteAsync($"{sql}", new { siteCode,project }, transaction, 0);
                    transaction.Commit();
                }

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        public async Task<Manifest?> GetById(Guid session)
        {
            return await _context.Manifests
                 .AsTracking().FirstOrDefaultAsync(x => x.Session == session);
        }

        public async Task Save(Manifest manifest)
        {
            await _context.Manifests.AddAsync(manifest);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Manifest manifest)
        {
            _context.Manifests.Update(manifest);
            await _context.SaveChangesAsync();
        }
    }
}
