using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataTypeDefaults : IEquatable<DataTypeDefaults>
    {
        internal const int DefaultSizeableSize = 1;
        internal const int DefaultPrecision = 18;
        internal const byte DefaultScale = 0;
        internal const int MaximumSizeIndicator = 0;
        private const int MaximumSizeIndicatorAlt = -1;
        private const byte MinimumPrecision = 1;
        private const byte MaximumPrecision = 38;

        public DataTypeDefaults(SqlDbType dataType)
        {
            SqlType = dataType;
        }

        public DataTypeDefaults(string dataType)
        {
            if (dataType.ToLowerInvariant() == "numeric") dataType = "decimal";
            SqlType = (SqlDbType)Enum.Parse(typeof(SqlDbType), dataType, true);
        }

        public SqlDbType SqlType { get; private set; }

        public override string ToString()
        {
            return SqlType.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DataTypeDefaults);
        }

        public bool Equals(DataTypeDefaults other)
        {
            if (other == null) return false;
            return SqlType == other.SqlType;
        }

        public override int GetHashCode()
        {
            return SqlType.GetHashCode();
        }

        public int? DefaultSize
        {
            get
            {
                switch (SqlType)
                {
                    case SqlDbType.Binary:
                    case SqlDbType.Char:
                    case SqlDbType.NChar:
                    case SqlDbType.NVarChar:
                    case SqlDbType.VarBinary:
                    case SqlDbType.VarChar:
                        return DefaultSizeableSize;
                    case SqlDbType.Decimal:
                        return DefaultPrecision;
                    default: return null;
                }
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
            return (decimalPlacesLeft ?? DefaultScale) == (decimalPlacesRight ?? DefaultScale);
        }

        public bool AreDecimalPlacesAllowed
        {
            get
            {
                return this.SqlType == SqlDbType.Decimal;
            }
        }

        public bool IsInteger
        {
            get
            {
                switch (this.SqlType)
                {
                    case SqlDbType.Int:
                    case SqlDbType.BigInt:
                    case SqlDbType.SmallInt:
                    case SqlDbType.TinyInt:
                        return true;
                    default: return false;
                }
            }
        }

        public bool IsSizeable
        {
            get 
            { 
                switch(this.SqlType)
                {
                    case SqlDbType.Binary:
                    case SqlDbType.Char:
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
                switch (this.SqlType)
                {
                    case SqlDbType.NChar:
                    case SqlDbType.NVarChar:
                        return true;
                    default: return false;
                }
            }
        }

        public bool IsDecimal
        {
            get { return SqlType == SqlDbType.Decimal; }
        }

        public bool IsMaximumSizeIndicator(int? size)
        {
            if (!size.HasValue) return false;
            return size == MaximumSizeIndicator || size == MaximumSizeIndicatorAlt;
        }

        public bool IsValidPrecision(int precision)
        {
            return precision >= MinimumPrecision && precision <= MaximumPrecision;
        }
    }
}
