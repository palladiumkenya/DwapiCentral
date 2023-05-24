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

        //act

        //Assert

    }
}
