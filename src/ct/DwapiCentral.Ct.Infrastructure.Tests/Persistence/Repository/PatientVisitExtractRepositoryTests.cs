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
    public async Task should_merge_new_visits()
    {
        //Arrange
        var newVisits = TestHelper.GetTestPatientVisitExtractsNew();

        //Act
        await _patientVisitExtractRepository.MergeAsync(newVisits);

        //Assert
        var savedPatientVisit = _context.PatientVisitExtracts.Find("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8",3, -10000);
        Assert.NotNull(savedPatientVisit);
    }

    [Test]
    public async Task should_Merge_AddUpdateVisits()
    {
        //Arrange
        var existingPatientVisits = TestHelper.GetTestPatientVisitExtractsUpdates();

        //Act
        await _patientVisitExtractRepository.MergeAsync(existingPatientVisits);

        //Assert
        var patientVisits = _context.PatientVisitExtracts.Find("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8", 1, -1000, DateTime.Today.AddDays(-2));
        Assert.AreEqual(163, patientVisits.Height);
        Assert.AreEqual(56, patientVisits.Weight);
    }
}
