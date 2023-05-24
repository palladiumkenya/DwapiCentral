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

        //act

        //Assert

    }
}


