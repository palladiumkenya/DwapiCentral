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
public class OvcRepositoryTests
{
    private CtDbContext _context;
    private IOvcRepository _ovcRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _ovcRepository = TestInitializer.ServiceProvider.GetService<IOvcRepository>();
    }

    [Test]
    public async Task should_Merge_OvcExtracts()
    {

        //Arrange
        var ovcExtracts = TestHelper.GetTestPatientOvcExtracts();



        //Act
        await _ovcRepository.MergeAsync(ovcExtracts);


        //Assert
        var savedPatientOvcExtracts = _context.OvcExtract.ToList();
        Assert.IsNotNull(savedPatientOvcExtracts);

    }
}
