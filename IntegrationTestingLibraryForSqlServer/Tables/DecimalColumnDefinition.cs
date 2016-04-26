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
        private byte precision = 18;
        private byte scale = 0;

        public DecimalColumnDefinition(string name)
            : base(name, SqlDbType.Decimal)
        {
        }

        public byte Precision
        {
            get { return precision; }
            set
            {
                if (value == 0 || value > 38) throw new ArgumentException("Precision must be between 1 and 38");
                precision = value;
                if (scale > precision) scale = precision;
            }
        }
        public byte Scale
        {
            get { return scale; }
            set
            {
                if (value > Precision) throw new ArgumentException("Scale must be between 0 and the precision");
                scale = value;
            }
        }

        public override bool Equals(ColumnDefinition other)
        {
            if (!base.Equals(other)) return false;
            var otherDecimal = other as DecimalColumnDefinition;
            if (otherDecimal == null) return false;
            if (Precision != otherDecimal.Precision) return false;
            if (Scale != otherDecimal.Scale) return false;
            return true;
        }

        public override bool IsValid()
        {
            if (!base.IsValid()) return false;
            if (IdentitySeed.HasValue && Scale > 0) return false;
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
