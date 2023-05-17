using DwapiCentral.Ct.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Ct.Application.Tests.Queries;

[TestFixture]
public class GetMasterFacilityQueryTests
{
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
    }

    [Test]
    public async Task should_Get_By_Code()
    {
        // Arrange
        var cmd = new GetMasterFacilityQuery(-10000);
        
        // Act
        var result = await _mediator.Send(cmd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}