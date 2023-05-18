using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class ManifestRepository:IManifestRepository
{
    private readonly CtDbContext _context;

    public ManifestRepository(CtDbContext context)
    {
        _context = context;
    }
    

    public async Task<Manifest> GetById(Guid id)
    {
       return await _context.Manifests
            .AsTracking().FirstOrDefaultAsync(x => x.Id == id);
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