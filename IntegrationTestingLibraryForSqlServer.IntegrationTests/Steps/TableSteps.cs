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

        [When(@"the table ""(.*)"" is created with a numeric column")]
        public void WhenTheTableIsCreatedWithANumericColumn(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
            definition.CreateOrReplaceWithDecimalsAsNumerics(database);
        }

        [Given(@"table ""(.*)"" is populated")]
        [When(@"table ""(.*)"" is populated")]
        public void WhenTableIsPopulated(string tableName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
            tableActions.Insert(tableName, tableData);
        }

        [When(@"table ""(.*)"" is populated supporting Null values")]
        public void WhenTableIsPopulatedSupportingNullValues(string tableName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
            tableData.TransformData(new TableDataNullValueTransformer());
            tableActions.Insert(tableName, tableData);
        }

        [When(@"a view called ""(.*)"" of the table ""(.*)"" is created")]
        public void WhenAViewCalledOfTheTableIsCreated(string viewName, string tableName)
        {
            var tableActions = new TableActions(database.ConnectionString);
            tableActions.CreateView(tableName, viewName);
        }

        [Then(@"the definition of table ""(.*)"" should match")]
        public void ThenTheDefinitionOfTableShouldMatch(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
            definition.VerifyMatch(database);
        }

        [Then(@"an attempt to create the table ""(.*)"" with an invalid definition should fail")]
        public void ThenAnAttemptToCreateTheTableWithAnInvalidDefinitionShouldFail(string tableName, Table table)
        {
            Exception ex = null;
            try
            {
                WhenTheTableIsCreatedWithANumericColumn(tableName, table);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex, ex == null ? "Exception not thrown when expected" : ex.Message);
        }

        [Then(@"the definition of table ""(.*)"" should contain")]
        public void ThenTheDefinitionOfTableShouldContain(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
            definition.VerifyMatchOrSubset(database);
        }

        [Then(@"the table ""(.*)"" should be populated with data")]
        public void ThenTheTableShouldBePopulatedWithData(string tableName, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));

            var actual = this.LoadTableDataFromSql(string.Format("SELECT * FROM {0}", tableName));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        [Then(@"the table ""(.*)"" should be populated with Id and dates")]
        public void ThenTheTableShouldBePopulatedWithIdAndDates(string tableName, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
            foreach (var row in expected.Rows)
            {
                if (row[1].ToString() == "NULL")
                    row[1] = DBNull.Value;
            }

            var actual = this.LoadTableDataFromSql(string.Format("SELECT * FROM {0}", tableName));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        [Then(@"the view ""(.*)"" filtered to id (.*) should be populated with data")]
        public void ThenTheViewFilteredToIdShouldBePopulatedWithData(string viewName, int id, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));

            var actual = this.LoadTableDataFromSql(string.Format("SELECT * FROM {0} WHERE Id = {1}", viewName, id));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        private TableData LoadTableDataFromSql(string sql)
        {
            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        return new DataReaderPopulatedTableData(reader);
                    }
                }
            }
        }
    }
}
