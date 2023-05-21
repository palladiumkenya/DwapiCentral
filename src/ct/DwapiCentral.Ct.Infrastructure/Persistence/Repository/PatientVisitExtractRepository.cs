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
        //_context.Database.GetDbConnection().BulkMerge(patientVisitExtracts);
        //var existingPatientVisits =  _context.PatientVisitExtracts
        //            .AsNoTracking()
        //            .ToList();

        //var existingPatientVisitsSet = new HashSet<PatientVisitExtract>(existingPatientVisits);

        //var newPatientVisits = new List<PatientVisitExtract>();

        //foreach (var visit in patientVisitExtracts)
        //{
        //    if (!existingPatientVisitsSet.Contains(visit))
        //        newPatientVisits.Add(visit);
        //}

        //_context.PatientVisitExtracts.AddRange(newPatientVisits);

        // _context.SaveChangesAsync();
       

       // _context.PatientVisitExtracts.AddRange(newPatientVisits);

        // _context.PatientVisitExtracts.AddRangeAsync(newPatientVisits);
         _context.SaveChangesAsync();


        return Task.CompletedTask;
    }
}
