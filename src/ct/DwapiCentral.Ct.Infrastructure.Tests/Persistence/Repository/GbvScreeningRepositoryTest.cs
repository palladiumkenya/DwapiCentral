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
public class GbvScreeningRepositoryTest
{
    private CtDbContext _context;
    private IGbvScreeningRepository _gbvScreeningRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _gbvScreeningRepository = TestInitializer.ServiceProvider.GetService<IGbvScreeningRepository>();
    }

    [Test]
    public async Task should_Merge_NewEnhancedAdherance()
    {
        //Arrange

        //act

        //Assert

    }
}
