using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureDefinition : IEquatable<ProcedureDefinition>
    {
        public ProcedureDefinition(DatabaseObjectName name)
            : this(name, null)
        {
        }
        public ProcedureDefinition(string name)
            : this(DatabaseObjectName.FromName(name), null)
        {
        }

        public ProcedureDefinition(DatabaseObjectName name, IEnumerable<ProcedureParameter> parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            Parameters = new ProcedureParameterCollection();
            if (parameters != null) Parameters.AddRange(parameters);
        }

        public DatabaseObjectName Name { get; private set; }
        public string Body { get; set; }
        public ProcedureParameterCollection Parameters { get;private set; }

        public bool HasBody
        {
            get { return !string.IsNullOrWhiteSpace(Body); }
        }

        public void VerifyEqual(ProcedureDefinition actual)
        {
            if (Equals(actual)) return;
            throw new EquivalenceException(MismatchExceptionMessage(actual));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + Name.Qualified);
            foreach (var definition in Parameters)
                sb.AppendLine(definition.ToString());
            sb.AppendLine("Body: " + Body);
            return sb.ToString();
        }

        public bool Equals(ProcedureDefinition other)
        {
            if (other == null) return false;
            if (Name.GetHashCode() != other.Name.GetHashCode()) return false;
            if (other.Body != null && !Body.Equals(other.Body, StringComparison.CurrentCultureIgnoreCase)) return false;
            return Parameters.ExceptReturnValue.SequenceEqual(other.Parameters.ExceptReturnValue);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProcedureDefinition);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        private string MismatchExceptionMessage(ProcedureDefinition actual)
        {
            return new StringBuilder()
                .AppendLine("Procedure mismatch.")
                .AppendLine("Expected:")
                .Append(this)
                .AppendLine("Actual:")
                .Append(actual)
                .ToString();
        }
    }
}
