using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class IntegerColumnDefinition : ColumnDefinition
    {
        private int? identitySeed;

        public IntegerColumnDefinition(string name, SqlDbType dataType)
            : base(name, dataType)
        {
            switch (dataType)
            {
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                    return;
                case SqlDbType.Decimal:
                    if (!(this is DecimalColumnDefinition)) throw new ArgumentException("Wrong datatype passed", nameof(dataType));
                    return;
                default:
                    if (dataType == SqlDbType.Decimal && this is DecimalColumnDefinition) return;
                    throw new ArgumentException("Wrong datatype passed", nameof(dataType));
            }
        }
        public virtual int? IdentitySeed
        {
            get { return identitySeed; }
            set
            {
                identitySeed = value;
                if (value.HasValue) AllowNulls = false;
            }
        }

        public override bool AllowNulls
        {
            get { return base.AllowNulls; }
            set
            {
                if (IdentitySeed.HasValue && value) throw new ArgumentException("Identity columns cannot be nullable");
                base.AllowNulls = value;
            }
        }

        public override bool Equals(ColumnDefinition other)
        {
            if (!base.Equals(other)) return false;
            var otherInteger = other as IntegerColumnDefinition;
            if (otherInteger == null) return false;
            if (IdentitySeed != otherInteger.IdentitySeed) return false;
            return true;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(base.ToString())
                .Append(", Identity Seed: " + IdentitySeed)
                .ToString();
        }
    }
}
