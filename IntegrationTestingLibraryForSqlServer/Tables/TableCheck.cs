
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
            expected.VerifyEqual(GetDefinition(expected.Name));
        }

        public void VerifyMatchOrSubset(TableDefinition expected)
        {
            expected.VerifyEqualOrSubsetOf(GetDefinition(expected.Name));
        }

        private TableDefinition GetDefinition(DatabaseObjectName tableName)
        {
            var factory = new TableDefinitionInterrogationStrategyFactory(connectionString);
            var strategy = factory.GetTableDefinitionInterrogationStrategy(TableDefinitionInterrogationStrategyType.SystemTables);
            return strategy.GetTableDefinition(tableName);
        }
    }
}
