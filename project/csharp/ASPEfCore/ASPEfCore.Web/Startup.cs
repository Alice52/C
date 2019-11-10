using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Logging;
using AspEfCore.Common.Configurations;
using AspEfCore.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using AspEfCore.Data.Db;
using AspEfCore.Data.Models;
using AspEfCore.Data.Repsoitories;
using EntityFramework.DbContextScope.Interfaces;
using EntityFramework.DbContextScope;
using Hangfire;
using Hangfire.SqlServer;
using System.Net.Http;
using AspEfCore.Web.Services;
using AspEfCore.Web.Jobs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;

namespace ASPEfCore.Web
{
    public class Startup
    {
       
        private readonly ILogger<Startup> _logger;
        //private readonly IHttpClientFactory _httpClientFactory;

        public IConfiguration Configuration { get; }
        public SwaggerConfig SwaggerConfig { get; set; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger
            //, IHttpClientFactory httpClientFactory
            )
        {
            Configuration = configuration;
            _logger = logger;
            //_httpClientFactory = httpClientFactory;

            SwaggerConfig = new SwaggerConfig();
            Configuration.GetSection("SwaggerConfig").Bind(SwaggerConfig);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // register Mvc
            services.AddMvc()
           .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
           {
               options.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
           });

            // Register DbContext: 
            // ServiceLifetime.Scoped: generate a new instance for per request
            // ServiceLifetime.Transient: generate a new instance for per used
            services.AddDbContext<CampDbContext>(
                options =>
                {
                    options.EnableSensitiveDataLogging(true);
                    options.UseSqlServer(Configuration.GetConnectionString("demo"),
                        option =>
                        {
                            option.CommandTimeout(30);
                            option.MaxBatchSize(1000);
                        });
                }, ServiceLifetime.Scoped);



            // Register Swagger
            InjectSwagger(services);
            // Register Repositiries
            InjectRepositiries(services);
            InjectDbContextScope(services);

            // Inject Hangfire
            InjectHangfire(services);

            InjectServices(services);
        }

        // config for UI
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            // HangFire UI
            app.UseHangfireDashboard("/hangfire", new DashboardOptions());

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                HeartbeatInterval = new System.TimeSpan(0, 1, 0),
                ServerCheckInterval = new System.TimeSpan(0, 1, 0),
                SchedulePollingInterval = new System.TimeSpan(0, 1, 0)
            });
            backgroundJobs.Schedule(() => Console.WriteLine("Hello world from Hangfire3!"), new System.TimeSpan(0, 1, 0));

           RecurringJob.AddOrUpdate(() => Console.WriteLine("Hello world RecurringJob!"), Cron.Minutely());


            // Swagger UI
            if (SwaggerConfig.IsEnabled)
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asp EFCore Web Service V1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Asp EFCore Web Service V2");
                    c.RoutePrefix = "swagger";
                });

                app.UseSwagger();
            }

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }


        //public async Task TestAsync()
        //{
        //    HttpClient client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri("http://api.github.com");
        //    string result = await client.GetStringAsync("/");
        //}

        private void InjectSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Asp EFCore Web Service Api", Version = "v1" });
                options.SwaggerDoc("v2", new Info { Title = "Asp EFCore Web Service Api", Version = "v2" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "AspEfCore.Web.xml");
                options.IncludeXmlComments(filePath);
            });
        }

        private void InjectRepositiries(IServiceCollection services)
        {
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IMajorRepository, MajorRepository>();
        }

        private void InjectDbContextScope(IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory, CampDbContextFactory>();
            services.AddSingleton<IDbContextScopeFactory, DbContextScopeFactory>();
            services.AddSingleton<IAmbientDbContextLocator, AmbientDbContextLocator>();

            services.AddScoped<IReadDbFacade, ReadDbFacade>();
        }

        private void InjectHangfire(IServiceCollection services)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));


            services.TryAddSingleton<IBackgroundJobFactory>(x => new CustomBackgroundJobFactory(
                       new BackgroundJobFactory(x.GetRequiredService<IJobFilterProvider>())));

            services.TryAddSingleton<IBackgroundJobPerformer>(x => new CustomBackgroundJobPerformer(
                new BackgroundJobPerformer(
                    x.GetRequiredService<IJobFilterProvider>(),
                    x.GetRequiredService<JobActivator>(),
                    TaskScheduler.Default)));

            services.TryAddSingleton<IBackgroundJobStateChanger>(x => new CustomBackgroundJobStateChanger(
                           new BackgroundJobStateChanger(x.GetRequiredService<IJobFilterProvider>())));

            services.AddHostedService<RecurringJobsService>();

            services.AddHangfireServer();
        }

        private void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IInsertDataService, InsertDataService>();
            //services.AddTransient<ReccuringJob, ReccuringJob>();
        }
    }
}
