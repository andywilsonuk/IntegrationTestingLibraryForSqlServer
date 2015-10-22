using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

        public TableDefinitionInterrogationStrategy GetTableDefinitionInterrogationStrategy(string viewName, string schemaName, TableDefinitionInterrogationStrategyType strategyType)
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
