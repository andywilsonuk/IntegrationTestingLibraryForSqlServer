using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DatabaseActionsExtensionsTests
    {
        [TestMethod]
        public void TableActionsReturnsCorrectlyScopedObject()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test2;Integrated Security=True";
            var target = new DatabaseActions(connectionString);

            var actual = target.TableActions();

            Assert.AreEqual(connectionString, actual.ConnectionString);
        }
    }
}
