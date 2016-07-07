using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DecimalColumnDefinition : IntegerColumnDefinition
    {
        private byte precision = DataType.DefaultPrecision;
        private byte scale = DataType.DefaultScale;

        public DecimalColumnDefinition(string name)
            : base(name, SqlDbType.Decimal)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsDecimal;

        public byte Precision
        {
            get { return precision; }
            set
            {
                if (!DataType.IsValidPrecision(value)) throw new ArgumentException("Precision must be between 1 and 38");
                precision = value;
                if (scale > precision) scale = precision;
            }
        }
        public byte Scale
        {
            get { return scale; }
            set
            {
                if (IdentitySeed.HasValue && value != DataType.DefaultScale) throw new ArgumentException("Cannot set Scale when column is identity");
                if (value > Precision) throw new ArgumentException("Scale must be between 0 and the precision");
                scale = value;
            }
        }

        public override int? IdentitySeed
        {
            get { return base.IdentitySeed; }
            set
            {
                if (value.HasValue) Scale = DataType.DefaultScale;
                base.IdentitySeed = value;
            }
        }

        public override bool Equals(ColumnDefinition other)
        {
            if (!base.Equals(other)) return false;
            var otherDecimal = (DecimalColumnDefinition)other;
            if (Precision != otherDecimal.Precision) return false;
            if (Scale != otherDecimal.Scale) return false;
            return true;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(base.ToString())
                .Append(", Precision: " + Precision)
                .Append(", Scale: " + Scale)
                .ToString();
        }
    }
}
