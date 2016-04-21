using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DomainAccount : IEquatable<DomainAccount>
    {
        public DomainAccount(string domain, string account)
            : this($"{domain}\\{account}")
        {
        }
        public DomainAccount(string qualifiedAccount)
        {
            Qualified = qualifiedAccount;
            Validate();
        }

        public string Qualified { get; private set; }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Qualified)) throw new ValidationException("Account name is blank");
            string validationText = $"Account name {Qualified} is invalid";
            if (Qualified.Count(x => x == '\\') != 1 || 
                Qualified.Substring(0, Qualified.IndexOf('\\')).Length == 0 ||
                Qualified.Substring(Qualified.IndexOf('\\') + 1).Length == 0)
                throw new ValidationException($"Account name {Qualified} is invalid");
        }

        public bool Equals(DomainAccount other)
        {
            return string.Equals(Qualified, other.Qualified, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
