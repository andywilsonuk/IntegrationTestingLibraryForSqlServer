using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests.Database
{
    [TestClass]
    public class DatabaseActionsTests
    {
        [TestMethod]
        public void DatabaseActionsConstructor()
        {
            string connectionString = @"server=(localdb)\v11.0;database=Test2;integrated security=True";

            DatabaseActions databaseActions = new DatabaseActions(connectionString);

            Assert.AreEqual(connectionString, databaseActions.ConnectionString);
        }
    }
}
