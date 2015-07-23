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

        public ColumnDefinition ToColumnDefinition(IDataRecord record)
        {
            this.record = record;
            return new ColumnDefinition(this.GetName(), this.GetDataType())
            {
                Size = this.GetSize(),
                Precision = this.GetPrecision(),
                AllowNulls = this.GetNullable(),
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
            switch (this.GetDataType())
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.Decimal:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    return this.record.GetInt16(Columns.Size);
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                    return this.record.GetInt16(Columns.Size) / 2;
                default: return (int?)null;
            }
        }

        private byte? GetPrecision()
        {
            switch (this.GetDataType())
            {
                case SqlDbType.Decimal: return this.record.GetByte(Columns.Precision);
                default: return null;
            }
        }

        private bool GetNullable()
        {
            return this.record.GetBoolean(Columns.IsNullable);
        }

        internal static class Columns
        {
            public const int Name = 0;
            public const int DataType = 1;
            public const int Size = 2;
            public const int Precision = 3;
            public const int IsNullable = 4;
        }
    }
}
