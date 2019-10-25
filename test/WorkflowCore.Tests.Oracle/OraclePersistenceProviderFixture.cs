using Oracle.ManagedDataAccess.Client;
using WorkflowCore.Interface;
using WorkflowCore.Persistence.EntityFramework.Services;
using WorkflowCore.Persistence.Oracle;
using WorkflowCore.UnitTests;
using Xunit;

namespace WorkflowCore.Tests.Oracle
{
    [Collection("Oracle collection")]
    public class OraclePersistenceProviderFixture : BasePersistenceFixture
    {
        private readonly string _connectionString;

        public OraclePersistenceProviderFixture(OracleDockerSetup setup)
        {
            _connectionString = OracleDockerSetup.ConnectionString;
        }

        protected override IPersistenceProvider Subject
        {
            get
            {
                var db = new EntityFrameworkPersistenceProvider(new OracleContextFactory(_connectionString), true, true);
                db.EnsureStoreExists();
                return db;
            }
        }
    }
}
