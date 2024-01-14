using System.Linq.Expressions;
using SharedKernel.Shared;

namespace SharedKernel.Abstractions;

public interface IBackgroundJobService
{
    string Enqueue(Expression<Func<Task>> methodCall);
    string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);
    string Schedule(Expression<Action> methodCall, TimeSpan delay);
    
    /// <summary>
    /// Add recurring job
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="methodCall"></param>
    /// <param name="cronFormat"><see cref="Cron"/> ></param>
    void RecurringJob(string jobName, Expression<Action> methodCall, string cronFormat);
    
    /// <summary>
    /// Add async recurring job
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="methodCall"></param>
    /// <param name="cronFormat"><see cref="Cron"/> ></param>
    void RecurringJob(string jobName, Expression<Func<Task>> methodCall, string cronFormat);
    void RemoveRecurringJob(string jobName);
}