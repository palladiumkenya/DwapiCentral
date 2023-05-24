using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
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
    public async Task Should_Merge_NewArtExtracts()
    {
        //Arrange

        

        //Act


        //Assert
    }
}
