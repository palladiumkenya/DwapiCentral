using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Application.Mappings;
using DwapiCentral.Ct.Application.Queries;
using DwapiCentral.Ct.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DwapiCentral.Ct.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        // add dependencies
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ValidateSiteCommand).Assembly));
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}