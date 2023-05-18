using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Context;

[TestFixture]
public class CtDbContextTests
{
    [Test]
    public void should_Seed()
    {
        // Arrange
        var context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        
        // Act
        context.EnsureSeeded();
        
        // Assert
        Assert.True(context.MasterFacilities.ToList().Count > 0);
    }
}