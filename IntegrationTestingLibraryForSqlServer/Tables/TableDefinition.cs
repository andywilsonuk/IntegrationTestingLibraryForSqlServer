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
        public TableDefinition(string name, string schema = Constants.DEFAULT_SCHEMA)
            : this(name, null, schema)
        {
        }

        public TableDefinition(string name, IEnumerable<ColumnDefinition> columns, string schema = Constants.DEFAULT_SCHEMA)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(schema)) throw new ArgumentNullException("schema");
            this.Name = name;
            this.Schema = schema;
            this.Columns = columns == null ? new List<ColumnDefinition>() : new List<ColumnDefinition>(columns);
        }

        public string Name { get; private set; }
        public string Schema { get; private set; }
        public ICollection<ColumnDefinition> Columns { get; private set; }

        public void EnsureValid()
        {
            if (this.Columns.Count == 0 || !this.Columns.All(x => x.IsValid()))
                throw new ValidationException("The definition is invalid " + Environment.NewLine + this.ToString());
        }

        public void VerifyEqual(TableDefinition other)
        {
            if (this.Equals(other)) return;
            throw new EquivalenceException(this.EquivalenceDetails(other));
        }

        public void VerifyEqualOrSubsetOf(TableDefinition superset)
        {
            if (this.Equals(superset)) return;
            if (this.IsSubset(superset)) return;
            throw new EquivalenceException(this.EquivalenceDetails(superset));
        }

        public bool IsSubset(TableDefinition superset)
        {
            if (superset == null) return false;
            if (!this.IsMatchingHashCodes(superset)) return false;
            return !this.Columns.Except(superset.Columns).Any();
        }

        public bool Equals(TableDefinition other)
        {
            if (other == null) return false;
            if (!this.IsMatchingHashCodes(other)) return false;
            if (this.Columns.Count != other.Columns.Count) return false;
            return !this.Columns.Except(other.Columns).Any();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TableDefinition);
        }

        public override int GetHashCode()
        {
            return this.Name.ToLowerInvariant().GetHashCode() ^
                   this.Schema.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + this.Name);
            sb.AppendLine("Schema: " + this.Schema);
            foreach (var definition in this.Columns)
                sb.Append(definition.ToString());
            return sb.ToString();
        }

        private bool IsMatchingHashCodes(TableDefinition other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        private string EquivalenceDetails(TableDefinition actual)
        {
            return new StringBuilder()
                .AppendLine("Table mismatch.")
                .AppendLine("Expected:")
                .Append(this.ToString())
                .AppendLine("Actual:")
                .Append(actual.ToString())
                .ToString();
        }
    }
}
