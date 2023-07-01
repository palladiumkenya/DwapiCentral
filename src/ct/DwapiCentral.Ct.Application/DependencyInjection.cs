using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.EventHandlers;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Application.Mappings;
using DwapiCentral.Ct.Application.Queries;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
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
        var configSection = configuration.GetSection("RabbitMQSettings");
        var settings = new RabbitOptions();
        
        services.AddSingleton<RabbitOptions>(settings);
        services.AddTransient(typeof(INotificationHandler<>), typeof(RabbitMQNotificationHandler<>));
        return services;
    }
}