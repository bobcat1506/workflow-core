using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.BackgroundTasks;

namespace WorkflowCore.Tests.Oracle
{
    internal class TestingWorkflowConsumer : WorkflowConsumer
    {
        public TestingWorkflowConsumer(IPooledObjectPolicy<IPersistenceProvider> persistencePoolPolicy, IQueueProvider queueProvider,
            ILoggerFactory loggerFactory, IServiceProvider serviceProvider, IWorkflowRegistry registry, IDistributedLockProvider lockProvider,
            IPooledObjectPolicy<IWorkflowExecutor> executorPoolPolicy, IDateTimeProvider datetimeProvider, WorkflowOptions options)
            : base(persistencePoolPolicy, queueProvider, loggerFactory, serviceProvider, registry, lockProvider, executorPoolPolicy, datetimeProvider, options)
        { }

        public async Task Execute(string id)
        {
            await this.ProcessItem(id, CancellationToken.None);
        }
    }
}