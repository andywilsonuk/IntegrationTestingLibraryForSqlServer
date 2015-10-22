
namespace IntegrationTestingLibraryForSqlServer
{
    public class ViewCheck
    {
        private string connectionString;

        public ViewCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void VerifyMatch(TableDefinition expected, TableDefinitionInterrogationStrategyType strategyType = TableDefinitionInterrogationStrategyType.DataReaderSchema)
        {
            expected.VerifyEqual(GetDefinition(expected.Name, expected.Schema, strategyType));
        }

        public void VerifyMatchOrSubset(TableDefinition expected, TableDefinitionInterrogationStrategyType strategyType = TableDefinitionInterrogationStrategyType.DataReaderSchema)
        {
            expected.VerifyEqualOrSubsetOf(GetDefinition(expected.Name, expected.Schema, strategyType));
        }

        private TableDefinition GetDefinition(string tableName)
        {
            return GetDefinition(tableName, Constants.DEFAULT_SCHEMA, TableDefinitionInterrogationStrategyType.DataReaderSchema);
        }

        private TableDefinition GetDefinition(string tableName, string schemaName)
        {
            return GetDefinition(tableName, schemaName, TableDefinitionInterrogationStrategyType.DataReaderSchema);
        }

        private TableDefinition GetDefinition(string tableName, TableDefinitionInterrogationStrategyType strategyType)
        {
            return GetDefinition(tableName, Constants.DEFAULT_SCHEMA, strategyType);
        }

        private TableDefinition GetDefinition(string tableName, string schemaName, TableDefinitionInterrogationStrategyType strategyType)
        {
            var factory = new TableDefinitionInterrogationStrategyFactory(connectionString);
            var strategy = factory.GetTableDefinitionInterrogationStrategy(tableName, schemaName, strategyType);
            return strategy.GetTableDefinition(tableName, schemaName);
        }
    }
}
