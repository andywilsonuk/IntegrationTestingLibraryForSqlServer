using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableCreateSqlGenerator
    {
        public string Sql(TableDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            definition.EnsureValid();
            return string.Format(CreateTableFormat, definition.Schema, definition.Name, this.CreateCommaSeparatedColumns(definition));
        }

        private string CreateCommaSeparatedColumns(TableDefinition definition)
        {
            return string.Join(",", definition.Columns.Select(x => this.GetFormattedColumnLine(x)));
        }

        private string GetFormattedColumnLine(ColumnDefinition column)
        {
            return string.Format(
                "[{0}] {1}{2}{3}",
                column.Name,
                this.GetFormattedDataType(column),
                this.GetFormattedIdentity(column),
                this.GetFormattedNullable(column));
        }

        private string GetFormattedDataType(ColumnDefinition column)
        {
            switch (column.DataType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                    if (!column.Size.HasValue) break;
                    return string.Format("{0}({1})", column.DataType, column.Size.Value);
                case SqlDbType.NVarChar:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    if (!column.Size.HasValue) break;
                    string size = column.IsMaximumSize ? "max" : column.Size.Value.ToString();
                    return string.Format("{0}({1})", column.DataType, size);
                case SqlDbType.Decimal:
                    if (!column.Size.HasValue) break;
                    return string.Format("{0}({1},{2})", column.DataType, column.Size.Value, column.Precision ?? 0);
            }
            return string.Format("{0}", column.DataType);
        }

        private string GetFormattedIdentity(ColumnDefinition column)
        {
            if (column.IdentitySeed != null)
                return string.Format(" IDENTITY({0},1) ", column.IdentitySeed);

            return " ";
        }

        private string GetFormattedNullable(ColumnDefinition column)
        {
            return column.AllowNulls ? "NULL" : "NOT NULL";
        }

        private const string CreateTableFormat = "CREATE TABLE [{0}].[{1}] ({2})";
    }
}
