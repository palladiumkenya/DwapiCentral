using Dapper;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository
{
    public class ManifestRepository : IManifestRepository
    {
        private readonly PrepDbContext _context;

        public ManifestRepository(PrepDbContext context)
        {
            _context = context;
        }

        public async Task ClearFacility(int siteCode)
        {
        //    var cons = _context.Database.GetDbConnection();

        //    var sql = @"

        //delete  from StageClients WHERE  SiteCode = @SiteCode;
        //delete  from StageClientLinkages WHERE  SiteCode = @SiteCode;
        //delete  from StageClientPartners WHERE  SiteCode = @SiteCode;
        //delete  from StageHtsClientTests WHERE  SiteCode = @SiteCode;
        //delete  from StageHtsClientTracing WHERE  SiteCode = @SiteCode;
        //delete  from StageHtsPartnerNotificationServices WHERE  SiteCode = @SiteCode;
        //delete  from StageHtsPartnerTracings WHERE  SiteCode = @SiteCode;
        //delete  from StageHtsTestKits WHERE  SiteCode = @SiteCode;
        

        //";
        //    try
        //    {

        //        if (cons.State != ConnectionState.Open)
        //            cons.Open();

        //        using (var transaction = cons.BeginTransaction())
        //        {
        //            await cons.ExecuteAsync($"{sql}", new { siteCode }, transaction, 0);
        //            transaction.Commit();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e.Message);
        //        throw;
        //    }
        }

        public async Task ClearFacility(int siteCode, string project)
        {
        //    var cons = _context.Database.GetDbConnection();

        //    var sql = @"

        //delete  from StageClients WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageClientLinkages WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageClientPartners WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageHtsClientTests WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageHtsClientTracing WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageHtsPartnerNotificationServices WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageHtsPartnerTracings WHERE  SiteCode = @SiteCode AND Project = @project;
        //delete  from StageHtsTestKits WHERE  SiteCode = @SiteCode AND Project = @project;
        

        //";
        //    try
        //    {

        //        if (cons.State != ConnectionState.Open)
        //            cons.Open();

        //        using (var transaction = cons.BeginTransaction())
        //        {
        //            await cons.ExecuteAsync($"{sql}", new { siteCode, project }, transaction, 0);
        //            transaction.Commit();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e.Message);
        //        throw;
        //    }
        }

        public async Task<Manifest?> GetById(Guid session)
        {
            return await _context.Manifests
                 .AsTracking().FirstOrDefaultAsync(x => x.Session == session);
        }

        public async Task<Guid> GetManifestId(int siteCode)
        {
            var sql = @"
                        SELECT TOP 1 Id 
                        FROM Manifests 
                        WHERE SiteCode = @siteCode AND ManifestStatus = @status
                        ORDER BY DateArrived DESC;
                    ";

            var parameters = new { siteCode, status = ManifestStatus.Staged };

            try
            {
                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    var manifestId = await connection.QueryFirstOrDefaultAsync<Guid>(sql, parameters);

                    return manifestId;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        public int GetPatientCount(Guid id)
        {
            
            var cargo = _context.Cargoes.FirstOrDefault(x => x.ManifestId == id && x.Type == CargoType.Patient);
            if (null != cargo)
                return cargo.Items.Split(",").Length;

            return 0;
        }


        public IEnumerable<Manifest> GetStaged(int siteCode)
        {
           
            var manifests = _context.Manifests.AsNoTracking().Where(x => x.ManifestStatus == ManifestStatus.Staged && x.SiteCode == siteCode)
                .ToList();

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = _context.Cargoes.AsNoTracking()
                    .Where(x => x.Type != CargoType.Patient && x.ManifestId == manifest.Id).ToList();
            }

            return manifests;
        }
        public void updateCount(Guid id, int clientCount)
        {
            var sql =
                $"UPDATE {nameof(_context.Manifests)} SET [{nameof(Manifest.Recieved)}]=@clientCount WHERE [{nameof(Manifest.Id)}]=@id";
            _context.Database.GetDbConnection().Execute(sql, new { id, clientCount });
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
