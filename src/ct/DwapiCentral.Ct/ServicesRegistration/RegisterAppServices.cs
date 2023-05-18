using System.Reflection;
using DwapiCentral.Ct.Application;
using DwapiCentral.Ct.Infrastructure;

namespace DwapiCentral.Ct.ServicesRegistration;

public static class RegisterAppServices
{
    public static IServiceCollection RegisterCtApp(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}