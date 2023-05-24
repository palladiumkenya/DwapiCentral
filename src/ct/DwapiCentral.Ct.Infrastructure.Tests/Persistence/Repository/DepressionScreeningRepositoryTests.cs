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
public class DepressionScreeningRepositoryTests
{
    private CtDbContext _context;
    private IDepressionScreeningRepository _depressionScreeningRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _depressionScreeningRepository = TestInitializer.ServiceProvider.GetService<IDepressionScreeningRepository>();
    }

    [Test]
    public async Task should_Merge_NewDefaulterTracing()
    {
        //Arrange

        //act

        //Assert

    }
}
