using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableActions
    {
        private string connectionString;

        public TableActions(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(TableDefinition definition)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new TableCreateSqlGenerator().Sql(definition));
            }
        }

        public void Drop(string name)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(dropTableCommand, name);
            }
        }

        public void CreateOrReplace(TableDefinition definition)
        {
            this.Drop(definition.Name);
            this.Create(definition);
        }

        public void Insert(string name, TableData table)
        {
            bool hasColumnNames = table.ColumnNames != null && table.ColumnNames != Enumerable.Empty<string>();
            var generator = new TableInsertSqlGenerator();
            using (var connection = new SqlConnection(this.connectionString))
            {
                foreach (var row in table.Rows)
                {
                    string command = hasColumnNames ? generator.Sql(name, table.ColumnNames) : generator.Sql(name, row.Count());

                    connection.ExecuteWithParameters(command, row);
                }
            }
        }

        public void CreateView(string tableName, string viewName)
        {
            var definition = new TableBackedViewDefinition(viewName, tableName);

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new TableBackedViewCreateSqlGenerator().Sql(definition));
            }
        }

        private const string dropTableCommand = @"if exists (select * from sys.objects where object_id = object_id('{0}') and type = (N'U')) drop table [{0}]";
    }
}
