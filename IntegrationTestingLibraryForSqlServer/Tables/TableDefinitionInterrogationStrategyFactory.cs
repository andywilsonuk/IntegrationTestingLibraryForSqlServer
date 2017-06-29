using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableDefinitionInterrogationStrategyFactory
    {
        string connectionString;

        public TableDefinitionInterrogationStrategyFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Invalid connection string", "SchemaTableDefinitionFactory");
            this.connectionString = connectionString;
        }

        public TableDefinitionInterrogationStrategy GetTableDefinitionInterrogationStrategy(TableDefinitionInterrogationStrategyType strategyType)
        {
            switch (strategyType)
            {
                case TableDefinitionInterrogationStrategyType.DataReaderSchema:
                    return new DataReaderSchemaTableDefinitionInterrogationStrategy(connectionString);
                case TableDefinitionInterrogationStrategyType.SystemTables:
                    return new SystemTablesTableDefinitionInterrogationStrategy(connectionString);
                default:
                    throw new ArgumentException("Invalid strategy type", "strategy type");
            }
        }
    }
}
