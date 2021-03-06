﻿using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Data.SqlClient;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class SchemaSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly DatabaseActions database;

        public SchemaSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            database = scenarioContext.Get<DatabaseActions>("Database");
        }

        [Given(@"the schema ""(.*)"" is created")]
        [When(@"the schema ""(.*)"" is created")]
        public void WhenTheSchemaIsCreated(string schemaName)
        {
            database.CreateSchema(schemaName);
        }

        [Then(@"the schema ""(.*)"" exists")]
        public void ThenTheSchemaExists(string schemaName)
        {
            Assert.True(SchemaExists(schemaName));
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

        [Given(@"the table ""(.*)"" is created in the schema ""(.*)""")]
        [When(@"the table ""(.*)"" is created in schema ""(.*)""")]
        public void GivenTheTableIsCreatedInTheSchema(string tableName, string schemaName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var def = new TableDefinition(new DatabaseObjectName(schemaName, tableName));
            def.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            tableActions.Create(def);
        }

        [When(@"the view ""(.*)"" of the table ""(.*)"" is created in the schema ""(.*)""")]
        public void WhenTheViewOfTheTableIsCreatedInTheSchema(string viewName, string tableName, string schemaName)
        {
            var tableActions = new TableActions(database.ConnectionString);
            tableActions.CreateView(new DatabaseObjectName(schemaName, tableName), new DatabaseObjectName(schemaName, viewName));
        }

        [Then(@"the table ""(.*)"" exists in the schema ""(.*)""")]
        public void ThenTheTableExistsInTheSchema(string tableName, string schemaName)
        {
            Assert.True(TableInSchemaExists(tableName, schemaName));
        }

        public bool TableInSchemaExists(string tableName, string schemaName)
        {
            using (var conn = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = string.Format(CheckTableInSchemaExistsScript, tableName, schemaName);
                    var result = command.ExecuteScalar();
                    return result is string && result.ToString() == tableName;
                }
            }
        }

        private const string CheckSchemaExistsScript = @"SELECT name FROM sys.schemas WHERE name = '{0}'";
        private const string CheckTableInSchemaExistsScript = @"
            SELECT t.name FROM sys.tables t
            JOIN sys.schemas s ON t.schema_id = s.schema_id and s.name = '{1}'
            WHERE t.name = '{0}'";
    }
}
