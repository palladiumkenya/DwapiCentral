using DwapiCentral.Prep.Application;
using DwapiCentral.Prep.Infrastructure;
using System.Reflection;


namespace DwapiCentral.Prep.ServicesRegistration;

public static class RegisterAppServices
{
    public static IServiceCollection RegisterPrepApp(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}