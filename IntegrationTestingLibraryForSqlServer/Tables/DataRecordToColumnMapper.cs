using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class DataRecordToColumnMapper
    {
        private IDataRecord record;
        private DataTypeDefaults dataTypeDefaults;

        public ColumnDefinition ToColumnDefinition(IDataRecord record)
        {
            this.record = record;
            var column = GetColumn();

            this.dataTypeDefaults = new DataTypeDefaults(column.DataType);
            column.AllowNulls = GetNullable();
            column.IdentitySeed = GetIdentitySeed();

            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn != null)
            {
                decimalColumn.Precision = GetPrecision();
                decimalColumn.Scale = GetScale();
                column.Size = decimalColumn.Precision;
            }
            else
            {
                column.Size = GetSize();
            }

            return column;
        }

        private string GetName()
        {
            return this.record.GetString(Columns.Name);
        }

        private ColumnDefinition GetColumn()
        {
            var dataTypeName = this.record.GetString(Columns.DataType);
            return new ColumnDefinitionFactory().FromSqlDbType(dataTypeName, GetName());
        }

        private int? GetSize()
        {
            if (this.dataTypeDefaults.IsUnicodeSizeAllowed) return this.record.GetInt16(Columns.Size) / 2;
            if (this.dataTypeDefaults.IsSizeAllowed)
            {
                return this.record.GetInt16(Columns.Size);
            }
            return (int?)null;
        }

        private byte GetScale()
        {
            return record.GetByte(Columns.Scale);
        }
        private byte GetPrecision()
        {
            return record.GetByte(Columns.Precision);
        }

        private bool GetNullable()
        {
            return this.record.GetBoolean(Columns.IsNullable);
        }

        private decimal? GetIdentitySeed()
        {
            if (this.record.GetBoolean(Columns.IsIdentity))
                return this.record.GetDecimal(Columns.IdentitySeed);
            return null;
        }

        internal static class Columns
        {
            public const int Name = 0;
            public const int DataType = 1;
            public const int Size = 2;
            public const int Precision = 3;
            public const int Scale = 4;
            public const int IsNullable = 5;
            public const int IsIdentity = 6;
            public const int IdentitySeed = 7;
        }
    }
}
