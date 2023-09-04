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
public class DefaulterTracingRepositoryTests
{
    private CtDbContext _context;
    private IDefaulterTracingRepository _defaulterTracingRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _defaulterTracingRepository = TestInitializer.ServiceProvider.GetService<IDefaulterTracingRepository>();
    }

    [Test]
    public async Task should_Merge_NewDefaulterTracing()
    {
        //Arrange
        var defaulterTracing = TestHelper.GetTestPatientDefaulterTracing();



        //Act
        await _defaulterTracingRepository.InsertExtract(defaulterTracing);


        //Assert
        var savedDefaulterTracingExtracts = _context.DefaulterTracingExtract.ToList();
        Assert.IsNotNull(savedDefaulterTracingExtracts);

    }
}
