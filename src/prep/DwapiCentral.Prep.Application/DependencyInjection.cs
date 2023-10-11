using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.Mappings;
using DwapiCentral.Shared.Domain.Model.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
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
            channel.ExchangeDeclare(rabbitMQConfig.ExchangeName, ExchangeType.Direct, false, false);


            // Register RabbitMQ services
            services.AddSingleton(connection);
            services.AddSingleton(channel);

            return services;
        }
    }
}
