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
            this.dataTypeDefaults = new DataTypeDefaults(this.GetDataType());
            return new ColumnDefinition
            {
                Name = this.GetName(),
                DataType = this.dataTypeDefaults.DataType,
                Size = this.GetSize(),
                Precision = this.GetPrecision(),
                AllowNulls = this.GetNullable(),
            };
        }

        private string GetName()
        {
            return (string)record[Columns.Name];
        }

        private SqlDbType GetDataType()
        {
            return (SqlDbType)record[Columns.DataType];
        }

        private int? GetSize()
        {
            return this.dataTypeDefaults.IsSizeAllowed ? (int)record[Columns.Size] : (int?)null;
        }

        private byte? GetPrecision()
        {
            return this.dataTypeDefaults.IsPrecisionAllowed ? Convert.ToByte(record[Columns.Precision]) : (byte?)null;
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
            public const string Precision = "NumericPrecision";
            public const string IsNullable = "AllowDBNull";
        }
    }
}
