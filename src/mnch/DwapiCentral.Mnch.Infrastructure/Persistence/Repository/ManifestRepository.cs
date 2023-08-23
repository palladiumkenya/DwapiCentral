using Dapper;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
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

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository
{
    public class ManifestRepository : IManifestRepository
    {
        private readonly MnchDbContext _context;

        public ManifestRepository(MnchDbContext context)
        {
            _context = context;
        }

        public async Task ClearFacility(int siteCode)
        {
            var cons = _context.Database.GetDbConnection();

            var sql = @"

        delete  from StageMnchPatients WHERE  SiteCode = @SiteCode;
        delete  from StageMnchEnrolments WHERE  SiteCode = @SiteCode;
        delete  from StageMnchArts WHERE  SiteCode = @SiteCode;
        delete  from StageAncVisits WHERE  SiteCode = @SiteCode;
        delete  from StageMatVisits WHERE  SiteCode = @SiteCode;
        delete  from StagePncVisits WHERE  SiteCode = @SiteCode;
        delete  from StageMotherBabyPairs WHERE  SiteCode = @SiteCode;
        delete  from StageCwcEnrolments WHERE  SiteCode = @SiteCode;
        delete  from StageCwcVisits WHERE  SiteCode = @SiteCode;
        delete  from StageHeis WHERE  SiteCode = @SiteCode;
        delete  from StageMnchLabs WHERE  SiteCode = @SiteCode;
        delete  from StageMnchImmunizations WHERE  SiteCode = @SiteCode;
        

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

        delete  from StageMnchPatients WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMnchEnrolments WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMnchArts WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageAncVisits WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMatVisits WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StagePncVisits WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMotherBabyPairs WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageCwcEnrolments WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageCwcVisits WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageHeis WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMnchLabs WHERE  SiteCode = @SiteCode AND Project = @project;
        delete  from StageMnchImmunizations WHERE  SiteCode = @SiteCode AND Project = @project;
        

        ";
            try
            {

                if (cons.State != ConnectionState.Open)
                    cons.Open();

                using (var transaction = cons.BeginTransaction())
                {
                    await cons.ExecuteAsync($"{sql}", new { siteCode, project }, transaction, 0);
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
