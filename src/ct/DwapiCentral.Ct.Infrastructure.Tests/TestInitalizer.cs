using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Infrastructure.Tests;

[SetUpFixture]
public class TestInitializer
{
    public static IServiceProvider ServiceProvider;

    [OneTimeSetUp]
    public void Init()
    {
        SetupDependencyInjection();
        InitDb();
    }

    private void SetupDependencyInjection()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        services.AddInfrastructure(config, true);
        ServiceProvider = services.BuildServiceProvider();
    }

    private void InitDb()
    {
        var dbs = ServiceProvider.GetService<CtDbContext>();
        dbs.Database.EnsureCreated();
        dbs.EnsureSeeded();
    }
}