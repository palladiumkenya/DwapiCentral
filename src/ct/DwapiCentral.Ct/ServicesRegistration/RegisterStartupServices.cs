using Hangfire.SqlServer;
using Hangfire;
using Serilog;
using Owin;
using DwapiCentral.Shared.Domain.Model.Common;

namespace DwapiCentral.Ct.ServicesRegistration;

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
           .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           }));

        GlobalConfiguration.Configuration.UseBatches();

        builder.Services.AddHangfireServer();

        var queues = new List<string>
            {
                "manifest", "patient", "patientart", "patientpharmacy", "patientvisits", "patientstatus",
                "covid","defaultertracing", "patientlabs", "patientbaselines", "patientadverseevents", "otz", "ovc",
                "depressionscreening", "drugalcoholscreening", "enhancedadherencecounselling", "gbvscreening", "ipt",
                "allergieschronicillness", "contactlisting", "default", "cervicalcancerscreening"
            };
        queues.ForEach(queue => ConfigureWorkers(builder.Configuration,new[] { queue.ToLower() }));



        builder.Services.RegisterCtApp(builder.Configuration);
            
            return builder;
        }

    private static void ConfigureWorkers(IConfiguration configuration, string[] queues)
    {

        var hangfireQueueOptions = new BackgroundJobServerOptions
        {
            ServerName = $"{Environment.MachineName}:{queues[0].ToUpper()}",
            WorkerCount = GetWorkerCount(configuration, queues[0]),
            Queues = queues,
            ShutdownTimeout = TimeSpan.FromMinutes(2),
        };

       
    }

    private static int GetWorkerCount(IConfiguration configuration, string queue)
    {
        int count = 5;

        try
        {
            var workerCount = configuration.GetSection("WorkerConfiguration")["Value"];
            var workers = workerCount.Split(',').ToList();
            var worker = workers.FirstOrDefault(x => x.Contains(queue));
            if (null != worker)
                int.TryParse(worker.Split('-')[1], out count);
        }
        catch (Exception e)
        {
            Log.Error("Error reading worker count", e);
        }

        return count;
    }
}