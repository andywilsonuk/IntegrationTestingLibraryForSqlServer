using System;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DomainAccount : IEquatable<DomainAccount>
    {
        private const char Seperator = '\\';

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

            int separatorIndex = Qualified.IndexOf(Seperator);

            if (Qualified.IndexOf('\\') == -1)
            {
                Qualified = Environment.UserDomainName + Seperator + Qualified;
                separatorIndex = Environment.UserDomainName.Length;
            }

            if (Qualified.Count(x => x == Seperator) != 1 || 
                Qualified.Substring(0, separatorIndex).Length == 0 ||
                Qualified.Substring(separatorIndex + 1).Length == 0)
                throw new ValidationException($"Account name {Qualified} is invalid");
        }

        public bool Equals(DomainAccount other)
        {
            return string.Equals(Qualified, other.Qualified, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
