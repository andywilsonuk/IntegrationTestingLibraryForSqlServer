using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ViewCheck
    {
        private string connectionString;

        public ViewCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void VerifyMatch(TableDefinition expected)
        {
            expected.VerifyEqual(this.GetDefinition(expected.Name));
        }

        public void VerifyMatchOrSubset(TableDefinition expected)
        {
            expected.VerifyEqualOrSubsetOf(this.GetDefinition(expected.Name));
        }

        public TableDefinition GetDefinition(string viewName)
        {
            var mapper = new SchemaRowToColumnMapper();
            var viewDefinition = new TableDefinition(viewName);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(ViewDefinitionQuery, viewName);
                    connection.Open();
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            foreach (DataRow row in reader.GetSchemaTable().Rows)
                            {
                                viewDefinition.Columns.Add(mapper.ToColumnDefinition(row));
                            }
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return viewDefinition;
        }

        private const string ViewDefinitionQuery = @"SELECT TOP 1 * FROM [{0}]";
    }
}
