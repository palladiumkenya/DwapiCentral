using Dapper;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class PatientVisitExtractRepository:IPatientVisitExtractRepository
{
    private readonly CtDbContext _context;

    public PatientVisitExtractRepository(CtDbContext context)
    {
        _context = context;
    }

   

    public Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtracts)
    {

        var distinctExtracts = patientVisitExtracts
                 .GroupBy(e => new { e.PatientPk, e.SiteCode,e.VisitId, e.VisitDate })
                 .Select(g => g.OrderByDescending(e => e.Id).First());

        _context.Database.GetDbConnection().BulkMerge(distinctExtracts);

        var extractIdsToKeep = distinctExtracts.Select(e => e.Id).ToList();
        var deleteQuery = $@"
                    DELETE FROM PatientVisitExtracts
                    WHERE Id NOT IN ({string.Join(",", extractIdsToKeep)})
                ";

        _context.Database.GetDbConnection().ExecuteAsync(deleteQuery);


        _context.SaveChangesAsync();
        return Task.CompletedTask;

    }
}
