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
            this.dataTypeDefaults = new DataTypeDefaults(this.GetDataType());
            return new ColumnDefinition
            {
                Name = this.GetName(),
                DataType = this.dataTypeDefaults.DataType,
                Size = this.GetSize(),
                Precision = this.GetPrecision(),
                AllowNulls = this.GetNullable(),
                IdentitySeed = this.GetIdentitySeed()
            };
        }

        private string GetName()
        {
            return this.record.GetString(Columns.Name);
        }

        private SqlDbType GetDataType()
        {
            return (SqlDbType)Enum.Parse(typeof(SqlDbType), this.record.GetString(Columns.DataType), true);
        }

        private int? GetSize()
        {
            if (this.dataTypeDefaults.IsUnicodeSizeAllowed) return this.record.GetInt16(Columns.Size) / 2;
            if (this.dataTypeDefaults.IsSizeAllowed) return this.record.GetInt16(Columns.Size);
            return (int?)null;
        }

        private byte? GetPrecision()
        {
            return this.dataTypeDefaults.IsPrecisionAllowed ? this.record.GetByte(Columns.Precision) : (byte?)null;
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
            public const int IsNullable = 4;
            public const int IsIdentity = 5;
            public const int IdentitySeed = 6;
        }
    }
}
