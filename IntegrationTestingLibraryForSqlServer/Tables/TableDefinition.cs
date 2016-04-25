using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableDefinition : IEquatable<TableDefinition>
    {
        public TableDefinition(string name)
            : this(DatabaseObjectName.FromName(name), null)
        {
        }
        public TableDefinition(DatabaseObjectName name)
            : this(name, null)
        {
        }

        public TableDefinition(DatabaseObjectName name, IEnumerable<ColumnDefinition> columns)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Columns = columns == null ? new List<ColumnDefinition>() : new List<ColumnDefinition>(columns);
        }

        public DatabaseObjectName Name { get; private set; }
        public ICollection<ColumnDefinition> Columns { get; private set; }

        public void EnsureValid()
        {
            if (Columns.Count == 0 || !Columns.All(x => x.IsValid()))
                throw new ValidationException("The definition is invalid " + Environment.NewLine + this.ToString());
        }

        public void VerifyEqual(TableDefinition other)
        {
            if (Equals(other)) return;
            throw new EquivalenceException(EquivalenceDetails(other));
        }

        public void VerifyEqualOrSubsetOf(TableDefinition superset)
        {
            if (Equals(superset)) return;
            if (IsSubset(superset)) return;
            throw new EquivalenceException(EquivalenceDetails(superset));
        }

        public bool IsSubset(TableDefinition superset)
        {
            if (superset == null) return false;
            if (!IsMatchingHashCodes(superset)) return false;
            return !Columns.Except(superset.Columns).Any();
        }

        public bool Equals(TableDefinition other)
        {
            if (other == null) return false;
            if (!IsMatchingHashCodes(other)) return false;
            if (Columns.Count != other.Columns.Count) return false;
            return !Columns.Except(other.Columns).Any();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TableDefinition);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + Name);
            foreach (var definition in Columns)
                sb.Append(definition);
            return sb.ToString();
        }

        private bool IsMatchingHashCodes(TableDefinition other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        private string EquivalenceDetails(TableDefinition actual)
        {
            return new StringBuilder()
                .AppendLine("Table mismatch.")
                .AppendLine("Expected:")
                .Append(this)
                .AppendLine("Actual:")
                .Append(actual)
                .ToString();
        }
    }
}
