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
                DecimalPlaces = this.GetDecimalPlaces(),
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
            if (this.dataTypeDefaults.IsSizeAllowed)
            {
                if (dataTypeDefaults.DataType == SqlDbType.Decimal)
                {
                    return Convert.ToInt32(record[Columns.Precision]);
                }
                else
                {
                    return (int?)record[Columns.Size];
                }
            }
            return (int?)null;
        }

        private byte? GetDecimalPlaces()
        {
            if (dataTypeDefaults.AreDecimalPlacesAllowed)
            {
                return Convert.ToByte(record[Columns.DecimalPlaces]);
            }
            return (byte?)null;
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
            public const string DecimalPlaces = "NumericScale";
            public const string Precision = "NumericPrecision";
            public const string IsNullable = "AllowDBNull";
        }
    }
}
