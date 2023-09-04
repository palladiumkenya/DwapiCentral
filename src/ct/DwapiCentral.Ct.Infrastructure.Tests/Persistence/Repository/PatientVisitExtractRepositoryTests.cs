using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

[TestFixture]
public class PatientVisitExtractRepositoryTests
{
    private IPatientVisitExtractRepository _patientVisitExtractRepository;
    private CtDbContext _context;


    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientVisitExtractRepository = TestInitializer.ServiceProvider.GetService<IPatientVisitExtractRepository>();
    
    }



    [Test]
    public async Task should_Merge_AddUpdateVisits()
    {
        //Arrange
        var existingPatientVisits = TestHelper.GetTestPatientVisitExtractsUpdates();

        //Act
        await _patientVisitExtractRepository.InsertExtract(existingPatientVisits);

        //Assert
        var patientVisits = _context.PatientVisitExtract.ToList();
        Assert.AreEqual(6,patientVisits.Count);
        
    }
}
