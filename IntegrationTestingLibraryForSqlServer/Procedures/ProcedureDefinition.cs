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
        public ProcedureDefinition(string name)
            : this(name, null)
        {
        }

        public ProcedureDefinition(string name, IEnumerable<ProcedureParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            this.Name = name;
            this.Parameters = parameters == null ? new List<ProcedureParameter>() : new List<ProcedureParameter>(parameters);
        }

        public string Name { get; private set; }
        public string Body { get; set; }
        public ICollection<ProcedureParameter> Parameters { get;private set; }
        public IEnumerable<ProcedureParameter> ParametersWithoutReturnValue
        {
            get { return this.Parameters.Where(x => x.Direction != ParameterDirection.ReturnValue); }
        }

        public void EnsureValid()
        {
            this.EnsureValid(false);
        }

        public void EnsureValid(bool bodyRequired)
        {
            bool isValid = bodyRequired ? !string.IsNullOrWhiteSpace(this.Body) : true;
            if (isValid) isValid = this.ParametersWithoutReturnValue.All(x => x.IsValid());
            if (!isValid) 
                throw new ValidationException("The definition is invalid " + Environment.NewLine + this.ToString());
        }

        public void VerifyEqual(ProcedureDefinition actual)
        {
            if (this.Equals(actual)) return;
            throw new EquivalenceException(this.MismatchExceptionMessage(actual));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + this.Name);
            foreach (var definition in this.Parameters)
                sb.AppendLine(definition.ToString());
            sb.AppendLine("Body: " + this.Body);
            return sb.ToString();
        }

        public bool Equals(ProcedureDefinition other)
        {
            if (other == null) return false;
            if (!this.Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (other.Body != null && !this.Body.Equals(other.Body, StringComparison.CurrentCultureIgnoreCase)) return false;
            return this.ParametersWithoutReturnValue.SequenceEqual(other.ParametersWithoutReturnValue);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProcedureDefinition);
        }

        public override int GetHashCode()
        {
            return this.Name.ToLowerInvariant().GetHashCode();
        }

        private string MismatchExceptionMessage(ProcedureDefinition actual)
        {
            return new StringBuilder()
                .AppendLine("Procedure mismatch.")
                .AppendLine("Expected:")
                .Append(this.ToString())
                .AppendLine("Actual:")
                .Append(actual.ToString())
                .ToString();
        }
    }
}
