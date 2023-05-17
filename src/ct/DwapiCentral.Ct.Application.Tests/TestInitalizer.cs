using DwapiCentral.Ct.Infrastructure;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Application.Tests;

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
        services.AddInfrastructure(config, true,GenerateDynamicConnection());
        services.AddApplication(config);
        ServiceProvider = services.BuildServiceProvider();
    }

    private void InitDb()
    {
        var dbs = ServiceProvider.GetService<CtDbContext>();
        dbs.Database.EnsureCreated();
        dbs.EnsureSeeded();
    }
    
    private string GenerateDynamicConnection()
    {
        RemoveTestsFilesDbs();
        var dir = $"{TestContext.CurrentContext.TestDirectory.HasToEndWith(@"/")}";
        var cn = $"DataSource={dir}TestArtifacts/Database/CentralDwapiTest.sqlite".Replace(".sqlite", $"{DateTime.Now.Ticks}.sqlite");
        return cn.ToOsStyle();
    }
        
    private void RemoveTestsFilesDbs()
    {
        string[] keyFiles = { "CentralDwapiTest.sqlite" };
        string[] keyDirs = { @"TestArtifacts/Database".ToOsStyle()};

        foreach (var keyDir in keyDirs)
        {
            DirectoryInfo di = new DirectoryInfo(keyDir);
            foreach (FileInfo file in di.GetFiles())
            {
                if (!keyFiles.Contains(file.Name))
                    file.Delete();
            }
        }
    }
}