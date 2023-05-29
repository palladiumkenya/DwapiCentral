using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Tests.Command;

[TestFixture]
public class MergePatientVisitCommandTests
{
    private IMediator _mediator;

    [SetUp]
    public void Setup()
    {
        _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        
    }

    [Test]
    public async Task should_merge_newPatientVisits()
    {
        //Arrange
        //var patientVisitExtract = TestHelper.GetTestPatientVisitExtractsNew();

        //Act
        //var result = await _mediator.Send(new MergePatientVisitCommand());

        //Assert
        //Assert.True(result.IsSuccess);
        
    }

    [Test]
    public async Task should_merge_existingPatientVisits()
    {
        //Arrange
        //var patientVisitExtract = TestHelper.GetTestPatientVisitExtractsUpdates();

        //Act
        //var result = await _mediator.Send(new MergePatientVisitCommand());

        //Assert
        //Assert.True(result.IsSuccess);
    }
}
