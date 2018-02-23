using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class ViewSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly DatabaseActions database;

        public ViewSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            database = scenarioContext.Get<DatabaseActions>("Database");
        }

        [When(@"the table-backed view ""(.*)"" is created")]
        public void WhenTheTable_BackedViewIsCreated(string viewName, Table table)
        {
            var definition = new TableDefinition("tbl" + viewName); 
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            definition.CreateOrReplace(database);
            definition.CreateView(database, viewName);
        }

        [Then(@"the definition of view ""(.*)"" should match")]
        public void ThenTheDefinitionOfViewShouldMatch(string viewName, Table table)
        {
            var definition = new TableDefinition(viewName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            var checker = new ViewCheck(database.ConnectionString);
            checker.VerifyMatch(definition);
        }

        [Then(@"the definition of view ""(.*)"" should match SystemTables definition")]
        public void ThenTheDefinitionOfViewShouldMatchSystemTablesDefinition(string viewName, Table table)
        {
            var definition = new TableDefinition(viewName);
            definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
            var checker = new ViewCheck(database.ConnectionString);
            checker.VerifyMatch(definition, TableDefinitionInterrogationStrategyType.SystemTables);
        }
    }
}
