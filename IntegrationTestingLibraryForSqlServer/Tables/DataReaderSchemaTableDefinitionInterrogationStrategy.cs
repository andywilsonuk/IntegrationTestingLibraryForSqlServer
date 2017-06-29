using System;
using System.Data;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataReaderSchemaTableDefinitionInterrogationStrategy : TableDefinitionInterrogationStrategy
    {
        string connectionString;
        private const string TableDefinitionQuery = @"SELECT * FROM {0} WHERE 1=0";

        public DataReaderSchemaTableDefinitionInterrogationStrategy(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Invalid connection string", "SchemaTableDefinitionFactory");
            this.connectionString = connectionString;
        }

        public TableDefinition GetTableDefinition(DatabaseObjectName viewName)
        {
            var mapper = new SchemaRowToColumnMapper();
            var viewDefinition = new TableDefinition(viewName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(TableDefinitionQuery, viewName.Qualified);
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
