using AspEfCore.Web.Services;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspEfCore.Web.Jobs
{
    public class ReccuringJob
    {
        private IInsertDataService _InsertDataService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReccuringJob(IHttpClientFactory httpClientFactory, IInsertDataService InsertDataService)
        {
            _httpClientFactory = httpClientFactory;
            _InsertDataService = InsertDataService;
        }

        public void reccuring()
        {
            //RecurringJob.AddOrUpdate(async () =>
            //{
            //    var client = _httpClientFactory.CreateClient();
            //    client.BaseAddress = new Uri("http://api.github.com");
            //    string result = await client.GetStringAsync("/");
            //});
            using (var server = new BackgroundJobServer())
            {
                //支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者
                BackgroundJob.Enqueue(() => Console.WriteLine("Simple111"));
                //延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行。   
                BackgroundJob.Schedule(() => Console.WriteLine("Reliable!"), TimeSpan.FromSeconds(5));
                //一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。
                RecurringJob.AddOrUpdate(() => Console.WriteLine("Transparent!"), Cron.Minutely);
                //Continuations: Continuations allow you to define complex workflows by chaining multiple background jobs together.
                //var jobId = BackgroundJob.Enqueue(() => Test("========First job"));
                //BackgroundJob.ContinueWith(jobId, () => Test("========Start execute next task"));
                Console.WriteLine("Hangfire Server started.Press any key to exit");
                Console.ReadKey();
            }
        }


    }
}
