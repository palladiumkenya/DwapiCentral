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
public class DrugAlcoholScreeningRepositoryTest
{
    private CtDbContext _context;
    private IDrugAlcoholScreeningRepository _drugAlcoholScreening;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _drugAlcoholScreening = TestInitializer.ServiceProvider.GetService<IDrugAlcoholScreeningRepository>();
    }

    [Test]
    public async Task should_Merge_NewDrugAlcoholExtracts()
    {
        //Arrange
        var drugAlcoholScreening = TestHelper.GetTestPatientDrugAlcoholScreening();



        //Act
        await _drugAlcoholScreening.MergeAsync(drugAlcoholScreening);


        //Assert
        var savedPatientDrugAlcoholExtracts = _context.DrugAlcoholScreeningExtract.ToList();
        Assert.IsNotNull(savedPatientDrugAlcoholExtracts);

    }
}
