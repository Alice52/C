using Hangfire;
using Hangfire.Annotations;
using Hangfire.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.SqlServer;
using Hangfire.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace AspEfCore.Web.Jobs
{
    public class RecurringJobsService : BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly IRecurringJobManager _recurringJobs;
        private readonly ILogger<RecurringJobScheduler> _logger;

        public RecurringJobsService(
            [NotNull] IBackgroundJobClient backgroundJobs,
            [NotNull] IRecurringJobManager recurringJobs,
            [NotNull] ILogger<RecurringJobScheduler> logger)
        {
            _backgroundJobs = backgroundJobs ?? throw new ArgumentNullException(nameof(backgroundJobs));
            _recurringJobs = recurringJobs ?? throw new ArgumentNullException(nameof(recurringJobs));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _backgroundJobs.Enqueue<Services>(x => x.LongRunning(JobCancellationToken.Null));

                //  [NotNull] string recurringJobId, [NotNull] Job job, [NotNull] string cronExpression, [NotNull] RecurringJobOptions options

                _recurringJobs.AddOrUpdate("seconds", () => TestAsync(), "*/15 * * * * *");
                //_recurringJobs.AddOrUpdate("minutely", () => Console.WriteLine("Hello, world!"), Cron.Minutely);
                //_recurringJobs.AddOrUpdate("hourly", () => Console.WriteLine("Hello"), "25 15 * * *");

                //_recurringJobs.AddOrUpdate("neverfires", () => Console.WriteLine("Can only be triggered"), "0 0 31 2 *");

                //_recurringJobs.AddOrUpdate("Hawaiian", () => Console.WriteLine("Hawaiian"), "15 08 * * *", TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time"));
                //_recurringJobs.AddOrUpdate("UTC", () => Console.WriteLine("UTC"), "15 18 * * *");
                _recurringJobs.AddOrUpdate("Russian", () => Console.WriteLine("Russian"), "15 21 * * *", TimeZoneInfo.Local);

                // http call
                // RecurringJob.AddOrUpdate(() => TestAsync(), Cron.Minutely());
            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred while creating recurring jobs.", e);
            }

            return Task.CompletedTask;
        }

        public String TestAsync()
        {
            //HttpClient client = new HttpClient(new HttpClientHandler());
            //var html = client.GetStringAsync(“https://blog.csdn.net/qq_36051316/article/details/84380024”).Result.ToString();
            //System.Console.WriteLine(html);

            HttpClient client = new HttpClient(new HttpClientHandler());
            var html = client.GetStringAsync("https://blog.csdn.net/qq_36051316/article/details/84380024").Result.ToString();
            //client.BaseAddress = new Uri("https://blog.csdn.net/qq_36051316/article/details/84380024");
            //string result = await client.GetStringAsync("/");
            Console.WriteLine(html);
            return html;
        }
    }
}