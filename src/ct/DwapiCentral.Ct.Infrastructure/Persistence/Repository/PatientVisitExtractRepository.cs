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

    public async Task<PatientVisitExtract> GetByPatientDetails(int patientPk, int siteCode, int visitId, DateTime visitDate)
    {
        //var query = @"
        //    SELECT * 
        //    FROM PatientVisitExtract
        //    where PatientPK = @PatientPK
        //    AND SiteCode = @SiteCode
        //    AND VisitId = @VisitId
        //    AND VisitDate = @VisitDate";

        //var parameters = new
        //{
        //    PatientPk = patientPk,
        //    SiteCode = siteCode,
        //    VisitId = visitId,
        //    visitDate = visitDate

        //};

        //var patientVisit = await _context.Database.GetDbConnection().QuerySingleOrDefaultAsync<PatientVisitExtract>(query, parameters);

        var patientVisit = await _context.PatientVisitExtracts
       .FirstOrDefaultAsync(p => p.PatientPk == patientPk && p.SiteCode == siteCode && p.VisitId == visitId && p.VisitDate == visitDate);

        return patientVisit;
    }

    public Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtracts)
    {
        
        _context.Database.GetDbConnection().BulkMerge(patientVisitExtracts);

        

         _context.SaveChangesAsync();


        return Task.CompletedTask;
    }
}
