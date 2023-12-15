using DwapiCentral.Hts.Application;
using DwapiCentral.Hts.Infrastructure;
using System.Reflection;


namespace DwapiCentral.Hts.ServicesRegistration;

public static class RegisterAppServices
{
    public static IServiceCollection RegisterCtApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}