using System;
using System.Data.SqlClient;

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
                IDENT_SEED('{0}')
            FROM sys.columns c
            INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
            WHERE c.object_id = OBJECT_ID('{0}')";

        public SystemTablesTableDefinitionInterrogationStrategy(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Invalid connection string", "SchemaTableDefinitionFactory");
            this.connectionString = connectionString;
        }

        public TableDefinition GetTableDefinition(DatabaseObjectName tableName)
        {
            var mapper = new DataRecordToColumnMapper();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return new TableDefinition(tableName,
                    connection.Execute(
                        (reader) => mapper.ToColumnDefinition(reader),
                        TableDefinitionQuery,
                        tableName.Qualified));
            }
        }
    }
}
