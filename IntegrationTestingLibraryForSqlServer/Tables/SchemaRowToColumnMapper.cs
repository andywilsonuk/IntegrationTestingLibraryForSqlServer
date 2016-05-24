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
        private ColumnDefinition column;

        public ColumnDefinition ToColumnDefinition(DataRow definitionRow)
        {
            record = definitionRow;
            GetColumn();
            GetNullable();
            GetSize();
            GetPrecision();
            GetScale();
            return column;
        }

        private string GetName()
        {
            return (string)record[Columns.Name];
        }

        private void GetColumn()
        {
            var factory = new ColumnDefinitionFactory();
            var dataType = new DataType((SqlDbType)record[Columns.DataType]);
            column = factory.FromDataType(dataType, GetName());
        }

        private void GetSize()
        {
            var sizeColumn = column as StringColumnDefinition;
            if (sizeColumn == null) return;
            int size = Convert.ToInt32(record[Columns.Size]);
            sizeColumn.Size = size == -1 ? 0 : size;
        }

        private void GetScale()
        {
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn == null) return;
            decimalColumn.Scale = Convert.ToByte(record[Columns.Scale]);
        }
        private void GetPrecision()
        {
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn == null) return;
            decimalColumn.Precision = Convert.ToByte(record[Columns.Precision]);
        }

        private void GetNullable()
        {
            column.AllowNulls = (bool)record[Columns.IsNullable];
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
