using DwapiCentral.Mnch.Application;
using DwapiCentral.Mnch.Infrastructure;
using System.Reflection;


namespace DwapiCentral.Mnch.ServicesRegistration;

public static class RegisterAppServices
{
    public static IServiceCollection RegisterMnchApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}