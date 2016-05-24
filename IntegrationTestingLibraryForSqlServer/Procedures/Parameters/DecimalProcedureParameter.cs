using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DecimalProcedureParameter : ProcedureParameter
    {
        private byte precision = DataType.DefaultPrecision;
        private byte scale = DataType.DefaultScale;

        public DecimalProcedureParameter(string name, ParameterDirection direction) 
            : base(name, SqlDbType.Decimal, direction)
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
                if (value > Precision) throw new ArgumentException("Scale must be between 0 and the precision");
                scale = value;
            }
        }

        public override bool Equals(ProcedureParameter other)
        {
            if (!base.Equals(other)) return false;
            var otherDecimal = (DecimalProcedureParameter)other;
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
