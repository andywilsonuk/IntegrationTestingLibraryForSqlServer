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
            var definition = new TableDefinition(tableName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            definition.CreateOrReplace(database);
        }

        [When(@"the table ""(.*)"" is created with a numeric column")]
        public void WhenTheTableIsCreatedWithANumericColumn(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            definition.CreateOrReplace(database);
        }

        [Given(@"table ""(.*)"" is populated")]
        [When(@"table ""(.*)"" is populated")]
        public void WhenTableIsPopulated(string tableName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
            tableActions.Insert(DatabaseObjectName.FromName(tableName), tableData);
        }

        [When(@"table ""(.*)"" is populated supporting Null values")]
        public void WhenTableIsPopulatedSupportingNullValues(string tableName, Table table)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
            tableActions.Insert(DatabaseObjectName.FromName(tableName), tableData);
        }

        [When(@"a view called ""(.*)"" of the table ""(.*)"" is created")]
        public void WhenAViewCalledOfTheTableIsCreated(string viewName, string tableName)
        {
            var tableActions = new TableActions(database.ConnectionString);
            tableActions.CreateView(DatabaseObjectName.FromName(tableName), DatabaseObjectName.FromName(viewName));
        }

        [Then(@"the definition of table ""(.*)"" should match")]
        public void ThenTheDefinitionOfTableShouldMatch(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            definition.VerifyMatch(database);
        }

        [Then(@"the definition of table ""(.*)"" should contain")]
        public void ThenTheDefinitionOfTableShouldContain(string tableName, Table table)
        {
            var definition = new TableDefinition(tableName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            definition.VerifyMatchOrSubset(database);
        }

        [Then(@"the table ""(.*)"" should be populated with data")]
        public void ThenTheTableShouldBePopulatedWithData(string tableName, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));

            var actual = LoadTableDataFromSql(string.Format("SELECT * FROM {0}", tableName));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        [Then(@"the table ""(.*)"" should be populated with Id and dates")]
        public void ThenTheTableShouldBePopulatedWithIdAndDates(string tableName, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));

            var actual = LoadTableDataFromSql(string.Format("SELECT * FROM {0}", tableName));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        [Then(@"the view ""(.*)"" filtered to id (.*) should be populated with data")]
        public void ThenTheViewFilteredToIdShouldBePopulatedWithData(string viewName, int id, Table table)
        {
            var expected = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));

            var actual = LoadTableDataFromSql(string.Format("SELECT * FROM {0} WHERE Id = {1}", viewName, id));

            expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
        }

        [When(@"the table ""(.*)"" is created outside of the library")]
        public void WhenTheTableIsCreatedOutsideOfTheLibrary(string tableName, Table table)
        {
            var columns = string.Join(",", table.CreateSet<ColumnDefinitionRaw>().Select(x => string.Format("[{0}] {1}({2},{3}){4}", x.Name, x.DataType, x.Size, x.DecimalPlaces, x.AllowNulls ? " NULL " : " NOT NULL")));

            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("CREATE TABLE {0} ({1})", tableName, columns);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
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
