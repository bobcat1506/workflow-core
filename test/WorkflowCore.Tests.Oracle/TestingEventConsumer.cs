using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.BackgroundTasks;

namespace WorkflowCore.Tests.Oracle
{
    internal class TestingEventConsumer : EventConsumer
    {
        public TestingEventConsumer(IPersistenceProvider persistenceStore, IQueueProvider queueProvider, ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider, IWorkflowRegistry registry, IDistributedLockProvider lockProvider, WorkflowOptions options,
            IDateTimeProvider datetimeProvider)
            : base(persistenceStore, queueProvider, loggerFactory, serviceProvider, registry, lockProvider, options, datetimeProvider)
        { }

        public async Task Execute(string id)
        {
            await this.ProcessItem(id, CancellationToken.None);
        }
    }
}