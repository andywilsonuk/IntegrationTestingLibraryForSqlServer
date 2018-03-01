
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        public TableDefinition GetDefinition(DatabaseObjectName tableName)
        {
            var factory = new TableDefinitionInterrogationStrategyFactory(connectionString);
            var strategy = factory.GetTableDefinitionInterrogationStrategy(TableDefinitionInterrogationStrategyType.SystemTables);
            return strategy.GetTableDefinition(tableName);
        }

        public TableData GetData(string tableName)
        {
            return GetData(new DatabaseObjectName(tableName));
        }

        public TableData GetData(DatabaseObjectName tableName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT * FROM {tableName.Qualified}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
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
