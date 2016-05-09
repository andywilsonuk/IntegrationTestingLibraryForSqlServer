using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ColumnDefinition : IEquatable<ColumnDefinition>
    {
        public string Name { get; private set; }
        public DataTypeDefaults DataType { get; private set; }
        public virtual bool AllowNulls { get; set; }

        public ColumnDefinition(string name, SqlDbType dataType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            DataType = new DataTypeDefaults(dataType);
            AllowNulls = true;

            if (!DataType.IsDecimal || this is DecimalColumnDefinition) return;
            if (!DataType.IsInteger || this is IntegerColumnDefinition) return;
            if (!DataType.IsSizeable || this is SizeableColumnDefinition) return;
            throw new ArgumentException("Wrong datatype passed", nameof(dataType));
        }

        public virtual bool Equals(ColumnDefinition other)
        {
            if (other == null) return false;
            if (!Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!DataType.Equals(other.DataType)) return false;
            if (AllowNulls != other.AllowNulls) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ColumnDefinition);
        }

        public override int GetHashCode()
        {
            return this.Name.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + this.Name)
                .Append(", Type: " + this.DataType)
                .Append(", Allow Nulls: " + this.AllowNulls)
                .ToString();
        }
    }
}
