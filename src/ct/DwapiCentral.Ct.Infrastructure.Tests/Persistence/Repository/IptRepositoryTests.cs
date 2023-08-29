using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using DwapiCentral.Ct.Infrastructure.Tests;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

[TestFixture]
public class IptRepositoryTests
{
    private CtDbContext _context;
    private IIptRepository _iptRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _iptRepository = TestInitializer.ServiceProvider.GetService<IIptRepository>();
    }

    [Test]
    public async Task should_Merge_IptExtracts()
    {
        //Arrange
        var iptExtracts = TestHelper.GetTestPatientIptExtracts();



        //Act
        await _iptRepository.MergeAsync(iptExtracts);


        //Assert
        var savedPatientIptExtracts = _context.IptExtract.ToList();
        Assert.IsNotNull(savedPatientIptExtracts);

    }
}
