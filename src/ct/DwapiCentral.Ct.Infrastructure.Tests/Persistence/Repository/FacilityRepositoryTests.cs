using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;

[TestFixture]
public class FacilityRepositoryTests
{
    private IFacilityRepository _facilityRepository;
    private CtDbContext _context;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _facilityRepository=TestInitializer.ServiceProvider.GetService<IFacilityRepository>();
    }
    [Test]
    public void should_Get_By_Code()
    {
        
       Assert.Pass();
    }

    [Test]
    public async Task should_Save()
    {
     
        // Arrange
        var facility = new Facility(1222, "Test");
        
        // Act
        await _facilityRepository.Save(facility);
        
        // Assert
        var savedFaclity = _context.Facilities.Find(1222);
        
        Assert.AreEqual(1222,savedFaclity.Code);
        Assert.AreEqual("Test",savedFaclity.Name);
        
    }
}