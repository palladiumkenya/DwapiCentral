using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

public class PatientStatusRepositoryTests
{
    private CtDbContext _context;
    private IPatientStatusRepository _patientStatusRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientStatusRepository = TestInitializer.ServiceProvider.GetService<IPatientStatusRepository>();
    }

    [Test]
    public async Task should_Merge_New_PatientStatus()
    {
        //Arrange

        //act

        //Assert

    }
}
