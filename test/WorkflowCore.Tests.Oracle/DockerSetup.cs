using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Docker.Testify;
using Xunit;

namespace WorkflowCore.Tests.Oracle
{
    public class OracleDockerSetup
    {
        public static string ConnectionString { get; set; }
        public static string ScenarioConnectionString { get; set; }

        static OracleDockerSetup()
        {
            //ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ubuntu-oracle)(PORT=32118)))" +
            //    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=WFC;Password=Oracle18;"; 
            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.56.102)(PORT=32118)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=WFC;Password=Oracle18;"; 
            ScenarioConnectionString = ConnectionString;
        }
    }

    //namespace WorkflowCore.Tests.Oracle
    //{
    //    public class OracleDockerSetup : DockerSetup
    //    {
    //        public static string ConnectionString { get; set; }
    //        public static string ScenarioConnectionString { get; set; }

    //        public override string ImageName => "microsoft/mssql-server-linux";
    //        public override int InternalPort => 1433;
    //        public override TimeSpan TimeOut => TimeSpan.FromSeconds(120);

    //        public const string SqlPassword = "I@mJustT3st1ing";

    //        public override IList<string> EnvironmentVariables => new List<string> { "ACCEPT_EULA=Y", $"SA_PASSWORD={SqlPassword}" };

    //        public override void PublishConnectionInfo()
    //        {
    //            ConnectionString = $"Server=127.0.0.1,{ExternalPort};Database=workflowcore-tests;User Id=sa;Password={SqlPassword};";
    //            ScenarioConnectionString = $"Server=127.0.0.1,{ExternalPort};Database=workflowcore-scenario-tests;User Id=sa;Password={SqlPassword};";
    //        }

    //        public override bool TestReady()
    //        {
    //            return true;
    //            try
    //            {
    //                var client = new SqlConnection($"Server=127.0.0.1,{ExternalPort};Database=master;User Id=sa;Password={SqlPassword};");
    //                client.Open();
    //                client.Close();
    //                return true;
    //            }
    //            catch
    //            {
    //                return false;
    //            }

    //        }
    //    }

    [CollectionDefinition("Oracle collection")]
    public class OracleCollection : ICollectionFixture<OracleDockerSetup>
    {
    }

}