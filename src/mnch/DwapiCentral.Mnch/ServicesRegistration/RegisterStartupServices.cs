using Hangfire.SqlServer;
using Hangfire;
using Serilog;
using DwapiCentral.Shared.Domain.Model.Common;
using DwapiCentral.Mnch.ServicesRegistration;
using DwapiCentral.Mnch.Filters;

namespace DwapiCentral.Mnch.ServicesRegistration;

public static class RegisterStartupServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var environment = builder.Environment;

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)    
                .AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            builder.Services.AddHangfire(configuration => configuration           
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseSqlServerStorage(builder.Configuration.GetConnectionString("LiveConnection"), new SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           }));

        #region hangfire
        Hangfire.GlobalConfiguration.Configuration.UseBatches(TimeSpan.FromDays(30));

        var queues = new List<string>
            {
                 "manifest","patientmnch", "ancvisit", "cwcenrollment","cwcvisit", "hei","matvisit","mnchart","mnchenrollment",
                  "immunization", "mnchlab","motherbabypair","pncvisit"
            };
        queues.ForEach(queue => ConfigureWorkers(builder.Configuration, builder.Services, new[] { queue.ToLower() }));

        #endregion



        builder.Services.RegisterMnchApp(builder.Configuration);
            
            return builder;
        }

    private static void ConfigureWorkers(IConfiguration configuration, IServiceCollection services, string[] queues)
    {

        var hangfireQueueOptions = new BackgroundJobServerOptions
        {
            ServerName = $"{Environment.MachineName}:{queues[0].ToUpper()}",
            WorkerCount = 5,
            Queues = queues,
            ShutdownTimeout = TimeSpan.FromMinutes(2),
        };

        services.AddHangfireServer(options =>
        {
            options.ServerName = hangfireQueueOptions.ServerName;
            options.WorkerCount = hangfireQueueOptions.WorkerCount;
            options.Queues = hangfireQueueOptions.Queues;
            options.ShutdownTimeout = hangfireQueueOptions.ShutdownTimeout;
        });

    }



}