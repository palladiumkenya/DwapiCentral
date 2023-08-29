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
[TestFixture]
public class CovidRepositoryTests
{
    private CtDbContext _context;
    private ICovidRepository _covidRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _covidRepository = TestInitializer.ServiceProvider.GetService<ICovidRepository>();
    }

    [Test]
    public async Task should_Merge_NewCovidExtracts()
    {
        //Arrange
        var covidExtract = TestHelper.GetTestPatientCovid();



        //Act
        await _covidRepository.MergeAsync(covidExtract);


        //Assert
        var savedCovidExtracts = _context.CovidExtract.ToList();
        Assert.IsNotNull(savedCovidExtracts);

    }
}


