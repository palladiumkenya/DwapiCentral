using DwapiCentral.Mnch.ServicesRegistration;

WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build()
    .SetupMiddleware()
    .Run();
