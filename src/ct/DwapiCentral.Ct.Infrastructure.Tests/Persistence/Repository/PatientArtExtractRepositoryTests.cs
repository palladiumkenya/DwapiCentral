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
public class PatientArtExtractRepositoryTests
{
    private CtDbContext _context;
    private IPatientArtExtractRepositorycs _patientArtRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientArtRepository = TestInitializer.ServiceProvider.GetService<IPatientArtExtractRepositorycs>();
    }

    [Test]
    public async Task Should_Merge_ArtExtracts()
    {
        //Arrange
        var patientArt = TestHelper.GetTestPatientArtUpdates();



        //Act
        await _patientArtRepository.MergPatientArt(patientArt);


        //Assert
        var savedPatientArtExtracts  = _context.PatientArtExtract.ToList();
        Assert.IsNotNull(savedPatientArtExtracts);
    }
}
