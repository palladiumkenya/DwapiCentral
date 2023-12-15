using Dapper;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class ManifestRepository:IManifestRepository
{
    private readonly CtDbContext _context;

    public ManifestRepository(CtDbContext context)
    {
        _context = context;
    }

    public async Task<Manifest> GetById(Guid session)
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

    public async Task<Guid> GetManifestId(int siteCode)
    {
        var sql = @"
                        SELECT TOP 1 Id 
                        FROM Manifests 
                        WHERE SiteCode = @siteCode AND UploadMode = @UploadMode
                        ORDER BY Created DESC;
                    ";

        var parameters = new { siteCode, UploadMode = UploadMode.DifferentialLoad };

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
}