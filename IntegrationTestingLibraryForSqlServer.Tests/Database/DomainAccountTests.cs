using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DomainAccountTests
    {
        const string domainName = "mydomain";
        const string accountName = "myaccount";
        readonly string qualifiedAccount = $"{domainName}\\{accountName}";

        [Fact]
        public void QualifiedConstructor()
        {
            DomainAccount account = new DomainAccount(qualifiedAccount);

            Assert.Equal(qualifiedAccount, account.Qualified);
        }

        [Fact]
        public void QualifiedConstructorMissingDomain()
        {
            DomainAccount account = new DomainAccount(accountName);

            Assert.Equal(Environment.UserDomainName + '\\' + accountName, account.Qualified);
        }

        [Fact]
        public void QualifiedConstructorBlankQualified()
        {
            Assert.Throws<ValidationException>(() => new DomainAccount(null));
        }

        [Fact]
        public void SplitConstructor()
        {
            DomainAccount account = new DomainAccount(domainName, accountName);

            Assert.Equal(qualifiedAccount, account.Qualified);
        }

        [Fact]
        public void Equality()
        {
            DomainAccount account1 = new DomainAccount(qualifiedAccount);
            DomainAccount account2 = new DomainAccount(qualifiedAccount);

            Assert.True(account1.Equals(account2));
        }

        [Fact]
        public void SplitConstructorMissingDomain()
        {
            Assert.Throws<ValidationException>(() => new DomainAccount(null, accountName));
        }

        [Fact]
        public void SplitConstructorMissingAccount()
        {
            Assert.Throws<ValidationException>(() => new DomainAccount(domainName, null));
        }
    }
}
