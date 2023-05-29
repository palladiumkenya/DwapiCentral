using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;
[TestFixture]
public class PatientAdverseEventRepositoryTests
{
    private CtDbContext _context;
    private IPatientAdverseEventRepository _patientAdverseRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientAdverseRepository = TestInitializer.ServiceProvider.GetService<IPatientAdverseEventRepository>();
    }

    [Test]
    public async Task should_Merge_PatientAdverseEvent()
    {
        //Arrange
        var patientAdverseEvent = TestHelper.GetTestPatientAdverseExtractsUpdates();



        //Act
        await _patientAdverseRepository.MergeAsync(patientAdverseEvent);


        //Assert
        var savedPatientAdverseExtracts = _context.PatientAdverseEventExtracts.ToList();
        Assert.IsNotNull(savedPatientAdverseExtracts);

    }
}
