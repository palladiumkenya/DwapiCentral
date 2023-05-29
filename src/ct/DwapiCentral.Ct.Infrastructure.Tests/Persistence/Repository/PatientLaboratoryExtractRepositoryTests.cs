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
public class PatientLaboratoryExtractRepositoryTests
{
    private CtDbContext _context;
    private IPatientLaboratoryExtractRepository _PatientLaboratoryExtractRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _PatientLaboratoryExtractRepository = TestInitializer.ServiceProvider.GetService<IPatientLaboratoryExtractRepository>();
    }

    [Test]
    public async Task should_Merge_UpdatepatientLabs()
    {
        //Arrange
        var patientLabs = TestHelper.GetTestPatientLabExtractsUpdates();

        //act

        await _PatientLaboratoryExtractRepository.MergeLaboratoryExtracts(patientLabs);

        //Assert
        var savedPatientLabs = _context.PatientLaboratoryExtracts.ToList();
        Assert.IsNotNull(savedPatientLabs);

    }
}
