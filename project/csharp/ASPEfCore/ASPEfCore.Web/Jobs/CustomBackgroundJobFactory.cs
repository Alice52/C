using Hangfire;
using Hangfire.Annotations;
using Hangfire.Client;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspEfCore.Web.Jobs
{
    public class CustomBackgroundJobFactory : IBackgroundJobFactory
    {
        private readonly IBackgroundJobFactory _inner;

        public CustomBackgroundJobFactory([NotNull] IBackgroundJobFactory inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public IStateMachine StateMachine => _inner.StateMachine;

        public BackgroundJob Create(CreateContext context)
        {
            Console.WriteLine($"Create: {context.Job.Type.FullName}.{context.Job.Method.Name} in {context.InitialState?.Name} state");
            return _inner.Create(context);
        }
    }
}
