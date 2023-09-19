using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository;
using DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;
[TestFixture]
public class AllergiesChronicIllnessRepositoryTests
{
  
        private CtDbContext _context;
        private IAllergiesChronicIllnessRepository _allergiesChronicIllnessRepository;

        [SetUp]
        public void Setup()
        {
            _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
            _allergiesChronicIllnessRepository = TestInitializer.ServiceProvider.GetService<IAllergiesChronicIllnessRepository>();
        }

        [Test]
        public async Task should_Merge_NewAllergiesChronicIllnesses()
        {
        //Arrange
        var allergiesChronicIllness = TestHelper.GetTestPatientAllergiesChronicIllness();



        //Act
        await _allergiesChronicIllnessRepository.InsertExtract(allergiesChronicIllness);


        //Assert
        var savedAllergiesChronicIllnessExtracts = _context.AllergiesChronicIllnessExtract.ToList();
        Assert.IsNotNull(savedAllergiesChronicIllnessExtracts);

    }
    

}
