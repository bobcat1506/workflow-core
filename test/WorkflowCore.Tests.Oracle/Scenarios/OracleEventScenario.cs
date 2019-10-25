using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkflowCore.IntegrationTests.Scenarios;
using WorkflowCore.Persistence.EntityFramework.Services;
using WorkflowCore.Persistence.Oracle;
using Xunit;

namespace WorkflowCore.Tests.Oracle.Scenarios
{
    [Collection("Oracle collection")]
    public class OracleEventScenario : EventScenario
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddWorkflow(x =>
            {
                x.UseOracle(OracleDockerSetup.ScenarioConnectionString, true, true);
                x.UseMaxConcurrentWorkflows(1);
                x.UsePollInterval(TimeSpan.FromSeconds(300));

                x.UsePersistence(sp => new EntityFrameworkPersistenceProvider(
                    new LoggingOracleContextFactory(OracleDockerSetup.ScenarioConnectionString, sp.GetRequiredService<ILoggerFactory>()),
                    true, true));
            });
        }
    }
}
