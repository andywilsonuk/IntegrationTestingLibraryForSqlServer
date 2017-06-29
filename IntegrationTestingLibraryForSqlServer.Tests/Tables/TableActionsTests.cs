using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableActionsTests
    {
        [TestMethod]
        public void TableActionsConnectionStringSet()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True";
            var target = new TableActions(connectionString);

            var actual = target.ConnectionString;

            Assert.AreEqual(connectionString, actual);
        }
    }
}
