using Dapper;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

[TestFixture]
public class PatientExtractRepositoryTests
{
    private IPatientExtractRepository _patientExtractRepository;
    private CtDbContext _context;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _patientExtractRepository=TestInitializer.ServiceProvider.GetService<IPatientExtractRepository>();
    }
    
    [Test]
    public async Task should_Merge_New()
    {
        //Arrange
        var newPatients = TestHelper.GetTestPatientExtractsNew();
       
        // Act
        await _patientExtractRepository.MergeAsync(newPatients);
        
        // Assert
        var savedPatient = _context.PatientExtract.Find(3, -10000);
        Assert.NotNull(savedPatient);
    }
    
    [Test]
    public async Task should_Merge_AddUpdate()
    {
        //Arrange
        var existingPatients = TestHelper.GetTestPatientExtractsUpdates();

        // Ensure _patientExtractRepository is initialized
      
        // Act
        await _patientExtractRepository.MergeAsync(existingPatients);
        
        // Assert  
        
        //var savedPatient = _context.Database.GetDbConnection().Query<PatientExtract>("write sql").FirstOrDefault();
        
        var savedPatient = _context.PatientExtract.Find(1, -10000);
        Assert.AreEqual("xC01",savedPatient.CccNumber);
        Assert.AreEqual("N01",savedPatient.Nupi);
    }
}
