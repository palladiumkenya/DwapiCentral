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
public class OtzRepositoryTests
{
    private CtDbContext _context;
    private IOtzRepository _otzRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _otzRepository = TestInitializer.ServiceProvider.GetService<IOtzRepository>();
    }

    [Test]
    public async Task should_Merge_OtzExtracts()
    { 
        //Arrange
        var otcExtracts = TestHelper.GetTestPatientOtzExtracts();



        //Act
        await _otzRepository.MergeAsync(otcExtracts);


        //Assert
        var savedPatientOtcExtracts = _context.OtzExtract.ToList();
        Assert.IsNotNull(savedPatientOtcExtracts);

    }
}
