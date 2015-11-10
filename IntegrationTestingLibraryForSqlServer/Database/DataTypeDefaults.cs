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

        public const int MaximumSizeIndicator = 0;

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
            return sizeLeft.HasValue && (sizeLeft.Value == MaximumSizeIndicator || sizeLeft.Value == -1) &&
                sizeRight.HasValue && (sizeRight.Value == 0 || sizeRight.Value == -1);
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
    }
}
