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
            : this(name, null)
        {
        }

        public TableDefinition(string name, IEnumerable<ColumnDefinition> columns)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            this.Name = name;
            this.Columns = columns == null ? new List<ColumnDefinition>() : new List<ColumnDefinition>(columns);
        }

        public string Name { get; private set; }
        public ICollection<ColumnDefinition> Columns { get; private set; }

        public void EnsureValid()
        {
            if (this.Columns.Count == 0 || !this.Columns.All(x => x.IsValid()))
                throw new ValidationException("The definition is invalid " + Environment.NewLine + this.ToString());
        }

        public void VerifyEqual(TableDefinition actual)
        {
            if (this.Equals(actual)) return;
            throw new EquivalenceException(this.EquivalenceDetails(actual));
        }

        public bool Equals(TableDefinition other)
        {
            if (!this.Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            return this.Columns.SequenceEqual(other.Columns);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TableDefinition);
        }

        public override int GetHashCode()
        {
            return this.Name.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + this.Name);
            foreach(var definition in this.Columns)
                sb.Append(definition.ToString());
            return sb.ToString();
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
