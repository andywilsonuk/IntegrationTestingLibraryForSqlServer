using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class ViewSteps
    {
        private DatabaseActions database = ScenarioContext.Current.Get<DatabaseActions>("Database");

        [When(@"the table-backed view ""(.*)"" is created")]
        public void WhenTheTable_BackedViewIsCreated(string viewName, Table table)
        {
            var definition = new TableDefinition("tbl" + viewName, table.CreateSet<ColumnDefinition>());
            definition.CreateOrReplace(database);
            definition.CreateView(this.database, viewName);
        }

        [Then(@"the definition of view ""(.*)"" should match")]
        public void ThenTheDefinitionOfViewShouldMatch(string viewName, Table table)
        {
            var definition = new TableDefinition(viewName, table.CreateSet<ColumnDefinition>());
            var checker = new ViewCheck(this.database.ConnectionString);
            checker.VerifyMatch(definition);
        }
    }
}
