using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class SystemTablesTableDefinitionInterrogationStrategy : TableDefinitionInterrogationStrategy
    {
        string connectionString;
        private const string TableDefinitionQuery = @"
            SELECT 
                c.name,
                t.name,
                c.max_length,
                c.precision,
                c.scale,
                c.is_nullable,
                c.is_identity,
                IDENT_SEED('[{0}].[{1}]')
            FROM sys.columns c
            INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
            WHERE c.object_id = OBJECT_ID('[{0}].[{1}]')";

        public SystemTablesTableDefinitionInterrogationStrategy(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Invalid connection string", "SchemaTableDefinitionFactory");
            this.connectionString = connectionString;
        }

        public TableDefinition GetTableDefinition(string tableName, string schemaName = Constants.DEFAULT_SCHEMA)
        {
            var mapper = new DataRecordToColumnMapper();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return new TableDefinition(tableName,
                    connection.Execute<ColumnDefinition>(
                        (reader) => mapper.ToColumnDefinition(reader),
                        TableDefinitionQuery,
                        schemaName, tableName), schemaName);
            }
        }
    }
}
