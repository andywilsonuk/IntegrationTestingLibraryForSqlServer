using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public abstract class VariableSizeColumnDefinition : ColumnDefinition
    {
        private int size = DataType.DefaultSize;

        public VariableSizeColumnDefinition(string name, SqlDbType dataType)
            : base(name, dataType)
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

        public override bool Equals(ColumnDefinition other)
        {
            if (!base.Equals(other)) return false;
            var otherSize = (VariableSizeColumnDefinition)other;
            if (IsMaximumSize && otherSize.IsMaximumSize) return true;
            if (Size != otherSize.Size) return false;
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
