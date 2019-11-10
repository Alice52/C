using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using Hangfire.States;

namespace AspEfCore.Web.Jobs
{
    public static class RecurringJobManagerExtensions
    {
        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Job job,
            [NotNull] string cronExpression)
        {
            AddOrUpdate(manager, recurringJobId, job, cronExpression, TimeZoneInfo.Utc);
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Job job,
            [NotNull] string cronExpression,
            [NotNull] TimeZoneInfo timeZone)
        {
            AddOrUpdate(manager, recurringJobId, job, cronExpression, timeZone, EnqueuedState.DefaultQueue);
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Job job,
            [NotNull] string cronExpression,
            [NotNull] TimeZoneInfo timeZone,
            [NotNull] string queue)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (timeZone == null) throw new ArgumentNullException(nameof(timeZone));
            if (queue == null) throw new ArgumentNullException(nameof(queue));

            manager.AddOrUpdate(
                recurringJobId,
                job,
                cronExpression,
                new RecurringJobOptions { QueueName = queue, TimeZone = timeZone });
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Action> methodCall,
            [NotNull] Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));
            AddOrUpdate(manager, recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Action<T>> methodCall,
            [NotNull] Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));
            AddOrUpdate(manager, recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Action> methodCall,
            [NotNull] string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (recurringJobId == null) throw new ArgumentNullException(nameof(recurringJobId));
            if (methodCall == null) throw new ArgumentNullException(nameof(methodCall));
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));

            var job = Job.FromExpression(methodCall);
            manager.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Action<T>> methodCall,
            [NotNull] string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (recurringJobId == null) throw new ArgumentNullException(nameof(recurringJobId));
            if (methodCall == null) throw new ArgumentNullException(nameof(methodCall));
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));

            var job = Job.FromExpression(methodCall);
            manager.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Func<Task>> methodCall,
            [NotNull] Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));
            AddOrUpdate(manager, recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Func<T, Task>> methodCall,
            [NotNull] Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));
            AddOrUpdate(manager, recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Func<Task>> methodCall,
            [NotNull] string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (recurringJobId == null) throw new ArgumentNullException(nameof(recurringJobId));
            if (methodCall == null) throw new ArgumentNullException(nameof(methodCall));
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));

            var job = Job.FromExpression(methodCall);
            manager.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            [NotNull] this IRecurringJobManager manager,
            [NotNull] string recurringJobId,
            [NotNull] Expression<Func<T, Task>> methodCall,
            [NotNull] string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (recurringJobId == null) throw new ArgumentNullException(nameof(recurringJobId));
            if (methodCall == null) throw new ArgumentNullException(nameof(methodCall));
            if (cronExpression == null) throw new ArgumentNullException(nameof(cronExpression));

            var job = Job.FromExpression(methodCall);
            manager.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }
    }
}
