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
        private ColumnDefinition column;

        public ColumnDefinition ToColumnDefinition(IDataRecord record)
        {
            this.record = record;
            GetColumn();
            GetNullable();
            GetSize();
            GetIdentitySeed();
            GetPrecision();
            GetScale();
            return column;
        }

        private string GetName()
        {
            return record.GetString(Columns.Name);
        }

        private void GetColumn()
        {
            var dataType = new DataType(record.GetString(Columns.DataType));
            column = new ColumnDefinitionFactory().FromDataType(dataType, GetName());
        }

        private void GetSize()
        {
            var sizeColumn = column as VariableSizeColumnDefinition;
            if (sizeColumn == null) return;

            int size = record.GetInt16(Columns.Size);
            if (size == -1)
            {
                sizeColumn.Size = 0;
                return;
            }
            sizeColumn.Size = column.DataType.IsUnicodeString ? size / 2 : size;
        }

        private void GetScale()
        {
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn == null) return;
            decimalColumn.Scale = record.GetByte(Columns.Scale);
        }
        private void GetPrecision()
        {
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn == null) return;
            decimalColumn.Precision = record.GetByte(Columns.Precision);
        }

        private void GetNullable()
        {
            column.AllowNulls = record.GetBoolean(Columns.IsNullable);
        }

        private void GetIdentitySeed()
        {
            var integerColumn = column as IntegerColumnDefinition;
            if (integerColumn == null) return;
            if (record.GetBoolean(Columns.IsIdentity)) integerColumn.IdentitySeed = (int?)record.GetDecimal(Columns.IdentitySeed);
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
