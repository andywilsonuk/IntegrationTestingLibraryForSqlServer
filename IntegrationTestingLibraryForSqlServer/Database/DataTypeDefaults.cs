using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class DataTypeDefaults
    {
        internal const int DefaultStringSize = 1;
        internal const int DefaultDecimalSize = 18;
        internal const byte DefaultNumberOfDecimalPlaces = 0;

        internal const int MaximumSizeIndicator1 = 0;
        internal const int MaximumSizeIndicator2 = -1;

        public DataTypeDefaults(SqlDbType dataType)
        {
            this.DataType = dataType;
        }

        public SqlDbType DataType { get; private set; }

        public int? DefaultSize
        {
            get
            {
                switch (this.DataType)
                {
                    case SqlDbType.Binary:
                    case SqlDbType.Char:
                    case SqlDbType.NChar:
                    case SqlDbType.NVarChar:
                    case SqlDbType.VarBinary:
                    case SqlDbType.VarChar:
                        return DefaultStringSize;
                    case SqlDbType.Decimal:
                        return DefaultDecimalSize;
                    default: return null;
                }
            }
        }

        public byte? DefaultDecimalPlaces
        {
            get
            {
                return this.DataType == SqlDbType.Decimal ? DefaultNumberOfDecimalPlaces : (byte?)null;
            }
        }

        public bool IsSizeEqual(int? sizeLeft, int? sizeRight)
        {
            if ((sizeLeft ?? DefaultSize) == (sizeRight ?? DefaultSize)) return true;
            return sizeLeft.HasValue && (this.IsMaximumSizeIndicator(sizeLeft)) &&
                sizeRight.HasValue && (this.IsMaximumSizeIndicator(sizeRight));
        }

        public bool AreDecimalPlacesEqual(byte? decimalPlacesLeft, byte? decimalPlacesRight)
        {
            return (decimalPlacesLeft ?? DefaultNumberOfDecimalPlaces) == (decimalPlacesRight ?? DefaultNumberOfDecimalPlaces);
        }

        public bool AreDecimalPlacesAllowed
        {
            get
            {
                return this.DataType == SqlDbType.Decimal;
            }
        }

        public bool IsIdentitySeedAllowed
        {
            get
            {
                switch (this.DataType)
                {
                    case SqlDbType.Int:
                    case SqlDbType.BigInt:
                    case SqlDbType.SmallInt:
                    case SqlDbType.TinyInt:
                    case SqlDbType.Decimal:
                        return true;
                    default: return false;
                }
            }
        }

        public bool IsSizeAllowed
        {
            get 
            { 
                switch(this.DataType)
                {
                    case SqlDbType.Binary:
                    case SqlDbType.Char:
                    case SqlDbType.Decimal:
                    case SqlDbType.VarBinary:
                    case SqlDbType.VarChar:
                    case SqlDbType.NChar:
                    case SqlDbType.NVarChar:
                        return true;
                    default: return false;
                }
            }
        }

        public bool IsUnicodeSizeAllowed
        {
            get
            {
                switch (this.DataType)
                {
                    case SqlDbType.NChar:
                    case SqlDbType.NVarChar:
                        return true;
                    default: return false;
                }
            }
        }

        public bool IsMaximumSizeIndicator(int? size)
        {
            if (!size.HasValue) return false;
            return size == MaximumSizeIndicator1 || size == MaximumSizeIndicator2;
        }
    }
}
