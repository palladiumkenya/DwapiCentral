using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

public class PatientBaseLinesRepositoryTests
{
    private CtDbContext _context;
    private IPatientBaseLinesRepository _patientBaselinesRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientBaselinesRepository = TestInitializer.ServiceProvider.GetService<IPatientBaseLinesRepository>();
    }

    [Test]
    public async Task should_Merge_PatientBaselines()
    {
        //Arrange
        var patientBaselines = TestHelper.GetTestPatientBaselinesExtractsUpdates();

        //act

        await _patientBaselinesRepository.MergeAsync(patientBaselines);

        //Assert
        var savedPatientBaselines = _context.PatientBaselinesExtract.ToList();
        Assert.IsNotNull(savedPatientBaselines);

    }
}
