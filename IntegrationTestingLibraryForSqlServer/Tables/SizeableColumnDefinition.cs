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
        private int size = 1;

        public SizeableColumnDefinition(string name, SqlDbType dataType)
            : base(name, dataType)
        {
            switch (dataType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                    return;
                default: throw new ArgumentException("Wrong datatype passed", nameof(dataType));
            }
        }

        public int Size
        {
            get { return size; }
            set
            {
                if (value < 0) throw new ArgumentException("Size must be greater than zero");
                size = value;
            }
        }

        public bool IsMaximumSize
        {
            get { return size == 0; }
            set { Size = 0; }
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
