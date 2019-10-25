using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Persistence.EntityFramework.Interfaces;
using WorkflowCore.Persistence.EntityFramework.Services;
using Xunit;

namespace WorkflowCore.Tests.Oracle.Scenarios
{
    [Collection("Oracle collection")]
    public class SimpleEventScenario
    {
        public class MyDataClass
        {
            public string StrValue1 { get; set; }
            public string StrValue2 { get; set; }
        }

        public class EventWorkflow : IWorkflow<MyDataClass>
        {
            public string Id => "EventWorkflow";
            public int Version => 1;
            public void Build(IWorkflowBuilder<MyDataClass> builder)
            {
                builder
                    .StartWith(context => ExecutionResult.Next())
                    .WaitFor("MyEvent", data => data.StrValue1, data => DateTime.Now)
                        .Output(data => data.StrValue1, step => step.EventData)
                    .WaitFor("MyEvent2", data => data.StrValue2)
                        .Output(data => data.StrValue2, step => step.EventData);
            }
        }

        internal IServiceProvider serviceProvider;

        public SimpleEventScenario()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow(o =>
            {
                o.UseOracle(OracleDockerSetup.ConnectionString, true, true);

                o.UsePersistence(sp => new EntityFrameworkPersistenceProvider(
                    new LoggingOracleContextFactory(OracleDockerSetup.ScenarioConnectionString, sp.GetRequiredService<ILoggerFactory>()),
                    true, true));
            });

            services.AddTransient<TestingEventConsumer>();
            services.AddTransient<TestingWorkflowConsumer>();

            serviceProvider = services.BuildServiceProvider();

            //config logging
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Debug);
            loggerFactory.AddDebug(LogLevel.Debug);

            //PersistenceProvider = serviceProvider.GetService<IPersistenceProvider>();
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<EventWorkflow, MyDataClass>();
            //Host.OnStepError += Host_OnStepError;
            //Host.Start();
        }

        [Fact]
        public async Task Scenario()
        {
            var persistenceProvider = serviceProvider.GetRequiredService<IPersistenceProvider>();
            var host = serviceProvider.GetRequiredService<IWorkflowHost>();

            var evtConsumer = serviceProvider.GetRequiredService<TestingEventConsumer>();
            var wfConsumer = serviceProvider.GetRequiredService<TestingWorkflowConsumer>();

            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();

            var workflowId = await host.StartWorkflow<MyDataClass>(new EventWorkflow().Id, new MyDataClass()
            {
                StrValue1 = id1,
                StrValue2 = id2
            });

            await wfConsumer.Execute(workflowId);
            await wfConsumer.Execute(workflowId);
            await wfConsumer.Execute(workflowId);

            await host.PublishEvent("MyEvent", id1, null);

            var eventId = (await persistenceProvider.GetEvents("MyEvent", id1, DateTime.UtcNow.AddDays(-1))).First();

            await evtConsumer.Execute(eventId);

            await wfConsumer.Execute(workflowId);

            await host.PublishEvent("MyEvent", id2, null);

            eventId = (await persistenceProvider.GetEvents("MyEvent", id2, DateTime.UtcNow.AddDays(-1))).First();

            await evtConsumer.Execute(eventId);

            await wfConsumer.Execute(workflowId);

            var wfInstance = persistenceProvider.GetWorkflowInstance(workflowId);
            Assert.True(wfInstance.Status == TaskStatus.RanToCompletion);
        }
    }
}
