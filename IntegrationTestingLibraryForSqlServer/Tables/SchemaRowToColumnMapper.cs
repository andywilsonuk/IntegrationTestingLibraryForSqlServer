using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class SchemaRowToColumnMapper
    {
        private DataRow record;
        private DataTypeDefaults dataTypeDefaults;

        public ColumnDefinition ToColumnDefinition(DataRow definitionRow)
        {
            this.record = definitionRow;
            var column = GetColumn();

            this.dataTypeDefaults = new DataTypeDefaults(column.DataType);
            column.AllowNulls = GetNullable();

            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn != null)
            {
                decimalColumn.Precision = GetPrecision();
                decimalColumn.Scale = GetScale();
            }
            var sizeableColumn = column as SizeableColumnDefinition;
            if (sizeableColumn != null)
            {
                sizeableColumn.Size = GetSize();
            }

            return column;
        }

        private string GetName()
        {
            return (string)record[Columns.Name];
        }

        private ColumnDefinition GetColumn()
        {
            return new ColumnDefinitionFactory().FromSqlDbType((SqlDbType)record[Columns.DataType], GetName());
        }

        private int GetSize()
        {
            int size = Convert.ToInt32(record[Columns.Size]);
            return size == -1 ? 0 : size;
        }

        private byte GetScale()
        {
            return Convert.ToByte(record[Columns.Scale]);
        }
        private byte GetPrecision()
        {
            return Convert.ToByte(record[Columns.Precision]);
        }

        private bool GetNullable()
        {
            return (bool)record[Columns.IsNullable];
        }

        internal static class Columns
        {
            public const string Name = "ColumnName";
            public const string DataType = "ProviderType";
            public const string Size = "ColumnSize";
            public const string Scale = "NumericScale";
            public const string Precision = "NumericPrecision";
            public const string IsNullable = "AllowDBNull";
        }
    }
}
