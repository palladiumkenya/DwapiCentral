using Dapper;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
         .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitId, e.VisitDate })
         .Select(g => g.OrderByDescending(e => e.Id).First())
         .ToList();

        var existingExtracts = _context.PatientVisitExtract
            .AsEnumerable()
            .Where(e => distinctExtracts.Any(d =>
                d.PatientPk == e.PatientPk &&
                d.SiteCode == e.SiteCode &&
                d.VisitId == e.VisitId &&
                d.VisitDate == e.VisitDate))
            .ToList();

        //determine the visits records to be inserted,updated and deleted

        var recordsToInsert = patientVisitExtracts.Except(existingExtracts, new PatientVisitEqualityComparer());
        var recordsToUpdate = patientVisitExtracts.Intersect(existingExtracts, new PatientVisitEqualityComparer());
        var recordsToDelete = patientVisitExtracts.Except(patientVisitExtracts, new PatientVisitEqualityComparer());

        // Perform the necessary operations in the SQL Server
         _context.Database.GetDbConnection().BulkInsert(recordsToInsert);
         _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
         _context.Database.GetDbConnection().BulkDelete(recordsToDelete);

     

        // var distinctToInsert = distinctExtracts
        //     .Where(d => !existingExtracts.Any(e =>
        //         d.PatientPk == e.PatientPk &&
        //         d.SiteCode == e.SiteCode &&
        //         d.VisitId == e.VisitId &&
        //         d.VisitDate == e.VisitDate))
        //     .ToList();
        // _context.Database.GetDbConnection().BulkMerge(distinctToInsert);

        // Perform deduplication after insertion
        //var duplicateIds = _context.PatientVisitExtracts
        //    .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitId, e.VisitDate })
        //    .Where(g => g.Count() > 1)
        //    .SelectMany(g => g.Skip(1))
        //    .Select(e => e.Id)
        //    .ToList();

        //if (duplicateIds.Any())
        //{
        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        var deleteQuery = $@"
        //    DELETE FROM PatientVisitExtracts
        //    WHERE Id IN ({string.Join(",", duplicateIds)})
        //    ";

        //        connection.ExecuteAsync(deleteQuery);
        //    }
        //}



        _context.SaveChangesAsync();
        
        return Task.CompletedTask;

    }
}
