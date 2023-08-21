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
        try
        {
            Hangfire.GlobalConfiguration.Configuration.UseBatches(TimeSpan.FromDays(30));
            ConfigureWorkers(builder.Configuration, builder.Services);
            GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Hangfire is down !");
        }
        #endregion



        builder.Services.RegisterCtApp(builder.Configuration);
            
            return builder;
        }

    private static void ConfigureWorkers(IConfiguration configuration,IServiceCollection services)
    {

        var hangfireQueueOptions = new BackgroundJobServerOptions
        {
            ServerName = "DWAPIMNCHMAIN",
            WorkerCount = 1,
           
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