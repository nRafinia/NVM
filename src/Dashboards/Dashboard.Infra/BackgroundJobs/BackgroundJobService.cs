using System.Linq.Expressions;
using Hangfire;
using SharedKernel.Abstractions;

namespace Dashboard.Infra.BackgroundJobs;

public class BackgroundJobService : IBackgroundJobService
{
    public string Enqueue(Expression<Func<Task>> methodCall)
        => BackgroundJob.Enqueue(methodCall);
    

    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay)
        => BackgroundJob.Schedule(methodCall, delay);
    

    public string Schedule(Expression<Action> methodCall, TimeSpan delay)
        => BackgroundJob.Schedule(methodCall, delay);
    

    public void RecurringJob(string jobName, Expression<Action> methodCall, string cronFormat)
    { 
        Hangfire.RecurringJob.AddOrUpdate(jobName,methodCall,cronFormat);
    }

    public void RecurringJob(string jobName, Expression<Func<Task>> methodCall, string cronFormat)
    {
        Hangfire.RecurringJob.AddOrUpdate(jobName,methodCall,cronFormat);
    }

    public void RemoveRecurringJob(string jobName)
    { 
        Hangfire.RecurringJob.RemoveIfExists(jobName);
    }
}