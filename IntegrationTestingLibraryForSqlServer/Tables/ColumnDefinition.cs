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
        public SqlDbType DataType { get; private set; }
        public int? Size { get; set; }
        public bool AllowNulls { get; set; }

        public ColumnDefinition(string name, SqlDbType dataType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            DataType = dataType;
            AllowNulls = true;
        }

        public virtual bool Equals(ColumnDefinition other)
        {
            if (other == null) return false;
            if (!this.Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (this.DataType != other.DataType) return false;
            var dataTypeDefaults = new DataTypeDefaults(this.DataType);
            if (!dataTypeDefaults.IsSizeEqual(this.Size, other.Size)) return false;
            if (this.AllowNulls != other.AllowNulls) return false;
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

        public virtual bool IsValid()
        {
            var dataTypeDefaults = new DataTypeDefaults(this.DataType);
            if (!dataTypeDefaults.IsSizeAllowed && this.Size.HasValue) return false;
            if (this.IsMaximumSize) return true;
            if (this.Size.HasValue && this.Size.Value < -1) return false;
            return true;
        }

        public void EnsureValid()
        {
            if (this.IsValid()) return;
            throw new ValidationException("Column definition is invalid." + Environment.NewLine + this.ToString());
        }

        public bool IsMaximumSize
        {
            get 
            {
                return new DataTypeDefaults(this.DataType).IsMaximumSizeIndicator(this.Size); 
            }
            set 
            {
                this.Size = DataTypeDefaults.MaximumSizeIndicator1;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + this.Name)
                .Append(", Type: " + this.DataType)
                .Append(", Size: " + this.Size)
                .Append(", Allow Nulls: " + this.AllowNulls)
                .ToString();
        }
    }
}
