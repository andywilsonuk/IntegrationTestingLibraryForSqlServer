using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DomainAccountTests
    {
        const string domainName = "mydomain";
        const string accountName = "myaccount";
        readonly string qualifiedAccount = $"{domainName}\\{accountName}";

        [TestMethod]
        public void QualifiedConstructor()
        {
            DomainAccount account = new DomainAccount(qualifiedAccount);

            Assert.AreEqual(qualifiedAccount, account.Qualified);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void QualifiedConstructorMissingDomain()
        {
            new DomainAccount(accountName);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void QualifiedConstructorBlankQualified()
        {
            new DomainAccount(null);
        }

        [TestMethod]
        public void SplitConstructor()
        {
            DomainAccount account = new DomainAccount(domainName, accountName);

            Assert.AreEqual(qualifiedAccount, account.Qualified);
        }

        [TestMethod]
        public void Equality()
        {
            DomainAccount account1 = new DomainAccount(qualifiedAccount);
            DomainAccount account2 = new DomainAccount(qualifiedAccount);

            Assert.IsTrue(account1.Equals(account2));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SplitConstructorMissingDomain()
        {
            new DomainAccount(null, accountName);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SplitConstructorMissingAccount()
        {
            new DomainAccount(domainName, null);
        }
    }
}
