using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public abstract class VariableSizeProcedureParameter : ProcedureParameter
    {
        private int size = DataType.DefaultSize;

        public VariableSizeProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        public int Size
        {
            get { return size; }
            set
            {
                if (DataType.IsMaximumSizeIndicator(value))
                {
                    IsMaximumSize = true;
                    return;
                }
                if (value < 1) throw new ArgumentException("Size must be greater than one");
                size = value;
            }
        }

        public bool IsMaximumSize
        {
            get { return size == DataType.MaximumSizeIndicator; }
            set { size = DataType.MaximumSizeIndicator; }
        }

        public override bool Equals(ProcedureParameter other)
        {
            if (!base.Equals(other)) return false;
            var otherSizeable = (VariableSizeProcedureParameter)other;
            if (IsMaximumSize && otherSizeable.IsMaximumSize) return true;
            if (Size != otherSizeable.Size) return false;
            return true;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(base.ToString())
                .Append(", Size: " + Size)
                .ToString();
        }
    }
}
