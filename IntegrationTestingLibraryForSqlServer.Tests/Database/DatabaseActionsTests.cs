using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DatabaseActionsTests
    {
        private SqlConnectionStringBuilder connectionDetails = 
            new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

        [TestMethod]
        public void DatabaseActions_Constructor()
        {
            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            string expected = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test2;Integrated Security=True";
            Assert.AreEqual(expected, databaseActions.ConnectionString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DatabaseActions_ConstructorMaster()
        {
            connectionDetails.InitialCatalog = "master";

            new DatabaseActions(connectionDetails.ToString());
        }

        [TestMethod]
        public void DatabaseActions_IsLocalDb()
        {
            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            Assert.IsTrue(databaseActions.IsLocalDB);
        }

        [TestMethod]
        public void DatabaseActions_IsLocalDbFalse()
        {
            connectionDetails.DataSource = "server1";

            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            Assert.IsFalse(databaseActions.IsLocalDB);
        }
    }
}
