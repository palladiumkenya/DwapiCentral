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
public class EnhancedAdherenceCounsellingRepositoryTests
{
    private CtDbContext _context;
    private IEnhancedAdherenceCounsellingRepository _enhancedAdheranceCouncellingRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _enhancedAdheranceCouncellingRepository = TestInitializer.ServiceProvider.GetService<IEnhancedAdherenceCounsellingRepository>();
    }

    [Test]
    public async Task should_Merge_NewEnhancedAdherance()
    {
        //Arrange

        //act

        //Assert

    }
}
