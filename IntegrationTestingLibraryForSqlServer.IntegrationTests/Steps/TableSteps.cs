using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class TableSteps
    {
        private DatabaseActions database = ScenarioContext.Current.Get<DatabaseActions>("Database");

        [Given(@"the table ""(.*)"" is created")]
        [When(@"the table ""(.*)"" is created")]
        public void WhenTheTableIsCreated(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
            definition.CreateOrReplace(database);
        }

        [Given(@"table ""(.*)"" is populated")]
        [When(@"table ""(.*)"" is populated")]
        public void WhenTableIsPopulated(string tableName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new TableData
            {
                ColumnNames = table.Header,
                Rows = table.Rows.Select(x => x.Values)
            };
            tableActions.Insert(tableName, tableData);
        }

        [Then(@"the definition of table ""(.*)"" should match")]
        public void ThenTheDefinitionOfTableShouldMatch(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
            definition.VerifyEqual(database);
        }

        [Then(@"the table ""(.*)"" should be populated with data")]
        public void ThenTheTableShouldBePopulatedWithData(string tableName, Table table)
        {
            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM {0}", tableName);
                    connection.Open();

                    int index = 0;
                    using (var reader = command.ExecuteReader())
                        while (reader.NextResult())
                        {
                            index++;
                            for (int i = 0; i < table.Rows[index].Values.Count; i++)
                            {
                                string expected = table.Rows[index].Values.ElementAt(i);
                                string actual = reader[i].ToString();
                                Assert.AreEqual(expected, actual);
                            }
                        }
                }
            }
        }

        [When(@"a view called ""(.*)"" of the table ""(.*)"" is created")]
        public void WhenAViewCalledOfTheTableIsCreated(string viewName, string tableName)
        {
            var tableActions = new TableActions(database.ConnectionString);
            tableActions.CreateView(tableName, viewName);
        }

        [Then(@"the view ""(.*)"" filtered to id (.*) should be populated with data")]
        public void ThenTheViewFilteredToIdShouldBePopulatedWithData(string viewName, int id, Table table)
        {
            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM {0} WHERE Id = {1}", viewName, id);
                    connection.Open();

                    int index = 0;
                    using (var reader = command.ExecuteReader())
                        while (reader.NextResult())
                        {
                            index++;
                            for (int i = 0; i < table.Rows[index].Values.Count; i++)
                            {
                                string expected = table.Rows[index].Values.ElementAt(i);
                                string actual = reader[i].ToString();
                                Assert.AreEqual(expected, actual);
                            }
                        }
                }
            }
        }

    }
}
