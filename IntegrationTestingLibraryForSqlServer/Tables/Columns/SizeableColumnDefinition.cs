using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class SizeableColumnDefinition : ColumnDefinition
    {
        private int size = DataTypeDefaults.DefaultSizeableSize;

        public SizeableColumnDefinition(string name, SqlDbType dataType)
            : base(name, dataType)
        {
            if (DataType.IsSizeable) return;
            throw new ArgumentException("Wrong datatype passed", nameof(dataType));
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
            get { return size == DataTypeDefaults.MaximumSizeIndicator; }
            set { size = DataTypeDefaults.MaximumSizeIndicator; }
        }

        public override bool Equals(ColumnDefinition other)
        {
            if (!base.Equals(other)) return false;
            var otherSizeable = other as SizeableColumnDefinition;
            if (otherSizeable == null) return false;
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
