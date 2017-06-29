using System;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class IntegerColumnDefinition : ColumnDefinition
    {
        private int? identitySeed;

        public IntegerColumnDefinition(string name, SqlDbType dataType)
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsInteger;

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
            var otherInteger = (IntegerColumnDefinition)other;
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
