using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests.Database
{
    [TestClass]
    public class SqlConnectionStringBuilderExtensionsTests
    {
        [TestMethod]
        public void SqlConnectionStringBuilderExtensions_IsMasterCatalog()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=master;integrated security=True");

            Assert.IsTrue(builder.IsMasterCatalog());
        }

        [TestMethod]
        public void SqlConnectionStringBuilderExtensions_IsMasterCatalogFalse()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

            Assert.IsFalse(builder.IsMasterCatalog());
        }

        [TestMethod]
        public void SqlConnectionStringBuilderExtensions_ToMasterCatalog()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

            SqlConnectionStringBuilder actual = builder.ToMasterCatalog();

            Assert.AreEqual(builder.DataSource, actual.DataSource);
            Assert.AreEqual("master", actual.InitialCatalog);
        }
    }
}
