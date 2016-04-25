
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
            expected.VerifyEqual(GetDefinition(expected.Name, strategyType));
        }
        public void VerifyMatchOrSubset(TableDefinition expected, TableDefinitionInterrogationStrategyType strategyType = TableDefinitionInterrogationStrategyType.DataReaderSchema)
        {
            expected.VerifyEqualOrSubsetOf(GetDefinition(expected.Name, strategyType));
        }

        private TableDefinition GetDefinition(DatabaseObjectName tableName, TableDefinitionInterrogationStrategyType strategyType)
        {
            var factory = new TableDefinitionInterrogationStrategyFactory(connectionString);
            var strategy = factory.GetTableDefinitionInterrogationStrategy(strategyType);
            return strategy.GetTableDefinition(tableName);
        }
    }
}
