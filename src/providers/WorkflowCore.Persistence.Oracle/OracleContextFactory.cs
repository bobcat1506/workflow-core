using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Persistence.EntityFramework.Interfaces;
using WorkflowCore.Persistence.EntityFramework.Services;

namespace WorkflowCore.Persistence.Oracle
{
    public class OracleContextFactory : IWorkflowDbContextFactory
    {
        private readonly string _connectionString;

        public OracleContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public WorkflowDbContext Build()
        {
            return new OracleContext(_connectionString);
        }
    }
}
