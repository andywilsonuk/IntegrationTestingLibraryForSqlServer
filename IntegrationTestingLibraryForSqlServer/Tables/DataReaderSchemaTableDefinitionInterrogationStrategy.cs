using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataReaderSchemaTableDefinitionInterrogationStrategy : TableDefinitionInterrogationStrategy
    {
        string connectionString;
        private const string TableDefinitionQuery = @"SELECT * FROM {0}.{1} WHERE 1=0";

        public DataReaderSchemaTableDefinitionInterrogationStrategy(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Invalid connection string", "SchemaTableDefinitionFactory");
            this.connectionString = connectionString;
        }

        public TableDefinition GetTableDefinition(string viewName, string schemaName)
        {
            var mapper = new SchemaRowToColumnMapper();
            var viewDefinition = new TableDefinition(new DatabaseObjectName(schemaName, viewName));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(TableDefinitionQuery, schemaName, viewName);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        foreach (DataRow row in reader.GetSchemaTable().Rows)
                        {
                            viewDefinition.Columns.Add(mapper.ToColumnDefinition(row));
                        }
                    }
                }
            }

            return viewDefinition;
        }
    }
}
