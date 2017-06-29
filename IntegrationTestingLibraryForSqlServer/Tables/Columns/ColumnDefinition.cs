using System;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public abstract class ColumnDefinition : IEquatable<ColumnDefinition>
    {
        public string Name { get; private set; }
        public DataType DataType { get; private set; }
        public virtual bool AllowNulls { get; set; }

        public ColumnDefinition(string name, SqlDbType dataType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            DataType = new DataType(dataType);
            AllowNulls = true;

            if (!IsDataTypeAllowed) throw new ArgumentException("Wrong datatype passed", nameof(dataType));
        }

        protected abstract bool IsDataTypeAllowed { get; }

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
            return Equals(obj as ColumnDefinition);
        }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + Name)
                .Append(", Type: " + DataType)
                .Append(", Allow Nulls: " + AllowNulls)
                .ToString();
        }
    }
}
