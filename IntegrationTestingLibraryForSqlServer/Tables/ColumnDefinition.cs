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
        public string Name { get; set; }
        public SqlDbType DataType { get; set; }
        public int? Size { get; set; }
        public byte? Precision { get; set; }
        public bool AllowNulls { get; set; }
        public decimal? IdentitySeed { get; set; }

        public ColumnDefinition()
        {
            this.AllowNulls = true;
        }

        public ColumnDefinition(string name, SqlDbType dataType)
            : this()
        {
            this.Name = name;
            this.DataType = dataType;
        }

        public bool Equals(ColumnDefinition other)
        {
            if (other == null) return false;
            if (!this.Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (this.DataType != other.DataType) return false;
            var dataTypeDefaults = new DataTypeDefaults(this.DataType);
            if (!dataTypeDefaults.IsSizeEqual(this.Size, other.Size)) return false;
            if (!dataTypeDefaults.IsPrecisionEqual(this.Precision, other.Precision)) return false;
            if (this.AllowNulls != other.AllowNulls) return false;
            if (this.IdentitySeed != other.IdentitySeed) return false;
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

        public bool IsValid()
        {
            var dataTypeDefaults = new DataTypeDefaults(this.DataType);
            if (string.IsNullOrWhiteSpace(this.Name)) return false;
            if (!dataTypeDefaults.IsSizeAllowed && this.Size.HasValue) return false;
            if (!dataTypeDefaults.IsPrecisionAllowed && this.Precision.HasValue) return false;
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
                return this.Size.HasValue && (this.Size == DataTypeDefaults.MaximumSizeIndicator || this.Size == -1); 
            }
            set 
            {
                this.Size = DataTypeDefaults.MaximumSizeIndicator;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + this.Name)
                .Append(", Type: " + this.DataType)
                .Append(", Size: " + this.Size)
                .Append(", Precision: " + this.Precision)
                .Append(", Allow Nulls: " + this.AllowNulls)
                .AppendLine(", Identity Seed: " + this.IdentitySeed)
                .ToString();
        }
    }
}
