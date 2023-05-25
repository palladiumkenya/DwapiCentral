using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

public class PatientBaseLinesRepositoryTests
{
    private CtDbContext _context;
    private IPatientBaseLinesRepository _patientBaselinesRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientBaselinesRepository = TestInitializer.ServiceProvider.GetService<IPatientBaseLinesRepository>();
    }

    [Test]
    public async Task should_Merge_New_PatientAdverse()
    {
        //Arrange

        //act

        //Assert

    }
}
