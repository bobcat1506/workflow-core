using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkflowCore.Persistence.EntityFramework.Interfaces;
using WorkflowCore.Persistence.EntityFramework.Services;
using WorkflowCore.Persistence.Oracle;

namespace WorkflowCore.Tests.Oracle
{
    internal class LoggingOracleContextFactory : IWorkflowDbContextFactory
    {
        private readonly string connectionString;
        private readonly ILoggerFactory loggerFactory;

        public LoggingOracleContextFactory(string connectionString, ILoggerFactory loggerFactory)
        {
            this.connectionString = connectionString;
            this.loggerFactory = loggerFactory;
        }

        public WorkflowDbContext Build()
        {
            var builder = new DbContextOptionsBuilder<OracleContext>();
            builder.UseLoggerFactory(loggerFactory);
            builder.EnableSensitiveDataLogging();
            builder.UseOracle(this.connectionString, o =>
            {

            });

            return new OracleContext(connectionString, builder.Options);
        }
    }
}