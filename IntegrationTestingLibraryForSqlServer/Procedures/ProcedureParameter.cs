using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureParameter : IEquatable<ProcedureParameter>
    {
        public ProcedureParameter()
        {
        }

        public ProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction)
        {
            this.Name = name;
            this.DataType = dataType;
            this.Direction = direction;
        }

        public string Name { get; set; }
        public SqlDbType DataType { get; set; }
        public int? Size { get; set; }
        public byte? Precision { get; set; }
        public ParameterDirection Direction { get; set; }

        public string QualifiedName
        {
            get { return this.Name.StartsWith("@") ? this.Name : "@" + this.Name; }
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Name)) return false;
            if (this.Size.HasValue && this.Size.Value < 1) return false;
            return true;
        }

        public void EnsureValid()
        {
            if (this.IsValid()) return;
            throw new ValidationException("Parameter definition is invalid." + Environment.NewLine + this.ToString());
        }

        public bool Equals(ProcedureParameter other)
        {
            if (other == null) return false;
            if (!this.QualifiedName.Equals(other.QualifiedName, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (this.DataType != other.DataType) return false;
            var dataTypeDefaults = new DataTypeDefaults(this.DataType);
            if (!dataTypeDefaults.IsSizeEqual(this.Size, other.Size)) return false;
            if (!dataTypeDefaults.IsPrecisionEqual(this.Precision, other.Precision)) return false;
            return this.IsDirectionEquivalent(other);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProcedureParameter);
        }

        public override int GetHashCode()
        {
            return this.Name.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + this.Name)
                .Append(", Data type: " + this.DataType)
                .Append(", Direction: " + this.Direction)
                .Append(", Size: " + this.Size)
                .Append(", Precision: " + this.Precision)
                .ToString();
        }

        public static ProcedureParameter FromSqlParameter(SqlParameter parameter)
        {
            return new ProcedureParameter
            {
                Name = parameter.ParameterName,
                DataType = parameter.SqlDbType,
                Size = parameter.Size == 0 ? (int?)null : parameter.Size,
                Precision = parameter.Precision == 0 ? (byte?)null : parameter.Precision,
                Direction = parameter.Direction
            };
        }

        private bool IsDirectionEquivalent(ProcedureParameter other)
        {
            if (this.Direction == other.Direction) return true;
            if (this.Direction == ParameterDirection.Output && other.Direction == ParameterDirection.InputOutput) return true;
            if (this.Direction == ParameterDirection.InputOutput && other.Direction == ParameterDirection.Output) return true;
            return false;
        }
    }
}
