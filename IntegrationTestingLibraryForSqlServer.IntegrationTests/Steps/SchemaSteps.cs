using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests.Steps
{
    [Binding]
    public class SchemaSteps
    {
        private DatabaseActions database = ScenarioContext.Current.Get<DatabaseActions>("Database");

        [When(@"the schema ""(.*)"" is created")]
        public void WhenTheSchemaIsCreated(string schemaName)
        {
            database.CreateSchema(schemaName);
        }

        [Then(@"the schema ""(.*)"" exists")]
        public void ThenTheSchemaExists(string schemaName)
        {
            Assert.IsTrue(SchemaExists(schemaName));
        }

        public bool SchemaExists(string schemaName)
        {
            using (var conn = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = string.Format(CheckSchemaExistsScript, schemaName);
                    var result = command.ExecuteScalar();
                    return result is string && result.ToString() == schemaName;
                }
            }
        }
        private const string CheckSchemaExistsScript = @"SELECT name FROM sys.schemas WHERE name = '{0}'";
    }
}
