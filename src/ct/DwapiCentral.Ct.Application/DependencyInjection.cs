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
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DwapiCentral.Ct.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        // add dependencies
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ValidateSiteCommand).Assembly));
        services.AddAutoMapper(typeof(MappingProfile));

        var rabbitMQConfig = new RabbitOptions();
        configuration.GetSection("RabbitMQ").Bind(rabbitMQConfig, options => options.BindNonPublicProperties = true);
        
        services.AddSingleton(rabbitMQConfig);

        // Create RabbitMQ connection and channel
        var factory = new ConnectionFactory
        {
            HostName = rabbitMQConfig.HostName,
            Port = rabbitMQConfig.Port,
            UserName = rabbitMQConfig.UserName,
            VirtualHost = rabbitMQConfig.VHost,
            Password = rabbitMQConfig.Password
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
       
        // Declare the exchange and queue
        
        channel.ExchangeDeclare(rabbitMQConfig.ExchangeName, ExchangeType.Direct,false,false);
        channel.QueueDeclare(rabbitMQConfig.QueueName, true, false, false, null);
        channel.QueueBind(rabbitMQConfig.QueueName, rabbitMQConfig.ExchangeName, "", null);

        // Register RabbitMQ services
        services.AddSingleton<IConnection>(connection);
        services.AddSingleton<IModel>(channel);


        services.AddTransient(typeof(INotificationHandler<>), typeof(RabbitMQNotificationHandler<>));
        return services;
    }
}