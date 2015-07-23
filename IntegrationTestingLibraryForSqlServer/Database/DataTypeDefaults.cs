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
        internal const int DefaultDecimalSize = 9;
        internal const byte DefaultDecimalPrecision = 18;
        private SqlDbType dataType;

        public DataTypeDefaults(SqlDbType dataType)
        {
            this.dataType = dataType;
        }

        public int? DefaultSize
        {
            get
            {
                switch (this.dataType)
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

        public byte? DefaultPrecision
        {
            get
            {
                return this.dataType == SqlDbType.Decimal ? DefaultDecimalPrecision : (byte?)null;
            }
        }

        public bool IsSizeEqual(int? sizeLeft, int? sizeRight)
        {
            return (sizeLeft ?? DefaultSize) == (sizeRight ?? DefaultSize);
        }

        public bool IsPrecisionEqual(byte? precisionLeft, byte? precisionRight)
        {
            return (precisionLeft ?? DefaultPrecision) == (precisionRight ?? DefaultPrecision);
        }
    }
}
