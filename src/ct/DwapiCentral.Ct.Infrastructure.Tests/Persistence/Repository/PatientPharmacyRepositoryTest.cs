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
public class PatientPharmacyRepositoryTest
{
    private CtDbContext _context;
    private IPatientPharmacyRepository _patientPharmacyRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientPharmacyRepository = TestInitializer.ServiceProvider.GetService<IPatientPharmacyRepository>();
    }

    [Test]
    public async Task Should_UpdatePharmacyExtracts()
    {
        //arrange 

        var patientPharmacy =  TestHelper.GetTestPatientPharmacyExtractsUpdates();

        //act

        await _patientPharmacyRepository.InsertExtract(patientPharmacy);

        



        //assert
        var PharmacyExtractsAdded = _context.PatientPharmacyExtract.ToList();
        Assert.NotNull(PharmacyExtractsAdded);



    }
}
