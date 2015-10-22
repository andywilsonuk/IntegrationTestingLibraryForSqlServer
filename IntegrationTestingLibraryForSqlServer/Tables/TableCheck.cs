
namespace IntegrationTestingLibraryForSqlServer
{
    public class TableCheck
    {
        private string connectionString;

        public TableCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void VerifyMatch(TableDefinition expected)
        {
            expected.VerifyEqual(GetDefinition(expected.Name, expected.Schema));
        }

        public void VerifyMatchOrSubset(TableDefinition expected)
        {
            expected.VerifyEqualOrSubsetOf(GetDefinition(expected.Name, expected.Schema));
        }

        private TableDefinition GetDefinition(string tableName, string schemaName = Constants.DEFAULT_SCHEMA)
        {
            var factory = new TableDefinitionInterrogationStrategyFactory(connectionString);
            var strategy = factory.GetTableDefinitionInterrogationStrategy(tableName, schemaName, TableDefinitionInterrogationStrategyType.SystemTables);
            return strategy.GetTableDefinition(tableName, schemaName);
        }
    }
}
