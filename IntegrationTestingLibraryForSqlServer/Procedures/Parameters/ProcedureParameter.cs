using System;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public abstract class ProcedureParameter : IEquatable<ProcedureParameter>
    {
        private string name;

        public ProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction)
        {
            Name = name;
            DataType = new DataType(dataType);
            Direction = direction;

            if (!IsDataTypeAllowed) throw new ArgumentException("Wrong datatype passed", nameof(dataType));
        }

        protected abstract bool IsDataTypeAllowed { get; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.StartsWith("@") ? value : "@" + value;
            }
        }
        public DataType DataType { get; private set; }
        public ParameterDirection Direction { get; set; }

        public virtual bool Equals(ProcedureParameter other)
        {
            if (other == null) return false;
            if (!Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!DataType.Equals(other.DataType)) return false;
            return IsDirectionEquivalent(other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProcedureParameter);
        }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Name: " + Name)
                .Append(", Data type: " + DataType)
                .Append(", Direction: " + Direction)
                .ToString();
        }

        private bool IsDirectionEquivalent(ProcedureParameter other)
        {
            if (Direction == other.Direction) return true;
            if (Direction == ParameterDirection.Output && other.Direction == ParameterDirection.InputOutput) return true;
            if (Direction == ParameterDirection.InputOutput && other.Direction == ParameterDirection.Output) return true;
            return false;
        }
    }
}
