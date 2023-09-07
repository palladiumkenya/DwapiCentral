using Dapper;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using DwapiCentral.Shared.Domain.Model.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
                        WHERE SiteCode = @siteCode
                        ORDER BY DateArrived DESC;
                    ";

            var parameters = new { siteCode, status = ManifestStatus.Processed };

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
            try
            {
                var cons = _context.Database.GetDbConnection();

                using (var transaction = cons.BeginTransaction())
                {
                    var cargo = _context.Database.GetDbConnection().QueryFirstOrDefault<Cargo>(

                    "SELECT * FROM Cargoes WHERE ManifestId = @ManifestId AND Type = @Type",

                    new { ManifestId = id, Type = CargoType.Patient },
                    transaction);

                    if (cargo != null)
                    {
                        var itemCount = cargo.Items.Split(',').Length;
                        transaction.Commit();
                        return itemCount;
                    }

                    transaction.Commit();
                    return 0;
                }
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
          
        }


        public IEnumerable<Manifest> GetStaged(int siteCode)
        {
            var cons = _context.Database.GetConnectionString();
            using (var connection = new SqlConnection(cons))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var manifests = connection.Query<Manifest>(
                            "SELECT * FROM Manifests WHERE ManifestStatus = @Status AND SiteCode = @SiteCode",
                            new { Status = ManifestStatus.Staged, SiteCode = siteCode },
                            transaction: transaction);

                        foreach (var manifest in manifests)
                        {
                            manifest.Cargoes = connection.Query<Cargo>(
                                "SELECT * FROM Cargoes WHERE Type != @CargoType AND ManifestId = @ManifestId",
                                new { CargoType = CargoType.Patient, ManifestId = manifest.Id },
                                transaction: transaction).ToList();
                        }

                        transaction.Commit();
                        return manifests;
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                        throw;
                    }
                }
            }
        }
        public void updateCount(Guid id, int clientCount)
        {
            var cons = _context.Database.GetConnectionString();
            var sql = @"
                            UPDATE 
                                    Manifests
                            SET 
                                    Recieved= @recieved ,
                                    ManifestStatus = @manifestStatus                           
                            WHERE 
                                    Id = @id";
            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {


                        connection.ExecuteAsync($"{sql}",
                            new
                            {
                               
                                recieved = clientCount,
                                manifestStatus = ManifestStatus.Processed
                               

                            }, transaction, 0);

                        transaction.Commit();
                    }
                }
            }catch(Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
                   
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
