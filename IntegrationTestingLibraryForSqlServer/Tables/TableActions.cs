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

        public void CreateWithDecimalsAsNumerics(TableDefinition definition)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new TableCreateSqlGenerator().SqlWithDecimalsAsNumerics(definition));
            }
        }

        public void Drop(string name)
        {
            Drop(DatabaseObjectName.FromName(name));
        }

        public void Drop(DatabaseObjectName name)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(dropTableCommand, name.Qualified);
            }
        }

        public void CreateOrReplace(TableDefinition definition)
        {
            Drop(definition.Name);
            Create(definition);
        }

        public void CreateOrReplaceWithDecimalsAsNumerics(TableDefinition definition)
        {
            Drop(definition.Name);
            CreateWithDecimalsAsNumerics(definition);
        }

        public void Insert(string name, TableData table, string schema = Constants.DEFAULT_SCHEMA)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (table == null) throw new ArgumentNullException("table");
            if (string.IsNullOrWhiteSpace(schema)) throw new ArgumentNullException("schema");

            bool hasColumnNames = table.ColumnNames != null && !table.ColumnNames.Equals(Enumerable.Empty<string>());
            var generator = new TableInsertSqlGenerator();
            using (var connection = new SqlConnection(this.connectionString))
            {
                foreach (var row in table.Rows)
                {
                    string command = hasColumnNames ? generator.Sql(name, table.ColumnNames, schema) : generator.Sql(name, row.Count(), schema);

                    connection.ExecuteWithParameters(command, row);
                }
            }
        }

        public void CreateView(string tableName, string viewName, string schema = Constants.DEFAULT_SCHEMA)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException("tableName");
            if (string.IsNullOrWhiteSpace(viewName)) throw new ArgumentNullException("viewName");
            if (string.IsNullOrWhiteSpace(schema)) throw new ArgumentNullException("schema");

            var definition = new TableBackedViewDefinition(new DatabaseObjectName(schema, viewName), new DatabaseObjectName(schema, tableName));

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new TableBackedViewCreateSqlGenerator().Sql(definition));
            }
        }

        private const string dropTableCommand = @"if exists (select * from sys.objects where object_id = object_id('{0}') and type = (N'U')) drop table {0}";
    }
}
