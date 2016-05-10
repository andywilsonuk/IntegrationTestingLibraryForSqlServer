using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataType : IEquatable<DataType>
    {
        internal const int DefaultSizeableSize = 1;
        internal const int DefaultPrecision = 18;
        internal const byte DefaultScale = 0;
        internal const int MaximumSizeIndicator = 0;
        private const int MaximumSizeIndicatorAlt = -1;
        private const byte MinimumPrecision = 1;
        private const byte MaximumPrecision = 38;

        public DataType(SqlDbType dataType)
        {
            SqlType = dataType;
        }

        public DataType(string dataType)
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
            return Equals(obj as DataType);
        }

        public bool Equals(DataType other)
        {
            if (other == null) return false;
            return SqlType == other.SqlType;
        }

        public override int GetHashCode()
        {
            return SqlType.GetHashCode();
        }

        internal int? DefaultSize
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

        internal bool IsInteger
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

        internal bool IsSizeable
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

        internal bool IsUnicodeSizeAllowed
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

        internal bool IsDecimal
        {
            get { return SqlType == SqlDbType.Decimal; }
        }

        internal bool IsMaximumSizeIndicator(int? size)
        {
            if (!size.HasValue) return false;
            return size == MaximumSizeIndicator || size == MaximumSizeIndicatorAlt;
        }

        internal bool IsValidPrecision(byte precision)
        {
            return precision >= MinimumPrecision && precision <= MaximumPrecision;
        }
    }
}
