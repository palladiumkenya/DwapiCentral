using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository;
[TestFixture]
public class ContactListingRepositoryTests
{
    private CtDbContext _context;
    private IContactListingRepository _contactListingRepository;

    [SetUp]
    public void Setup()
    {
        _context = TestInitializer.ServiceProvider.GetService<CtDbContext>();
        _contactListingRepository = TestInitializer.ServiceProvider.GetService<IContactListingRepository>();
    }

    [Test]
    public async Task should_Merge_NewContactListings()
    {
        //Arrange

        //act

        //Assert

    }
}
