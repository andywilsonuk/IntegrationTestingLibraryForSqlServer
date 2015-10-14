using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            expected.VerifyEqual(this.GetDefinition(expected.Name, expected.Schema));
        }

        public void VerifyMatchOrSubset(TableDefinition expected)
        {
            expected.VerifyEqualOrSubsetOf(this.GetDefinition(expected.Name, expected.Schema));
        }

        public TableDefinition GetDefinition(string tableName, string schemaName = Constants.DEFAULT_SCHEMA)
        {
            var mapper = new DataRecordToColumnMapper();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                return new TableDefinition(tableName, 
                    connection.Execute<ColumnDefinition>(
                        (reader) => mapper.ToColumnDefinition(reader),
                        TableDefinitionQuery,
                        schemaName, tableName), schemaName);
            }
        }

        private const string TableDefinitionQuery = @"
SELECT 
    c.name,
    t.name,
    c.max_length,
    c.precision,
    c.is_nullable,
    c.is_identity,
    IDENT_SEED('[{0}].[{1}]')
FROM sys.columns c
INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('[{0}].[{1}]')";
    }
}
