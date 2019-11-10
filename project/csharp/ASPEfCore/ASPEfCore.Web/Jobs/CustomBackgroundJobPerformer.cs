using Hangfire.Annotations;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspEfCore.Web.Jobs
{
    public class CustomBackgroundJobPerformer : IBackgroundJobPerformer
    {
        private readonly IBackgroundJobPerformer _inner;

        public CustomBackgroundJobPerformer([NotNull] IBackgroundJobPerformer inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public object Perform(PerformContext context)
        {
            Console.WriteLine($"Perform {context.BackgroundJob.Id} ({context.BackgroundJob.Job.Type.FullName}.{context.BackgroundJob.Job.Method.Name})");
            return _inner.Perform(context);
        }
    }
}