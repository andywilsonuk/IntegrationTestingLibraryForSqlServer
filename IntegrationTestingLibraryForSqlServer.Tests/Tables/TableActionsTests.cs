using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableActionsTests
    {
        [Fact]
        public void TableActionsConnectionStringSet()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True";
            var target = new TableActions(connectionString);

            var actual = target.ConnectionString;

            Assert.Equal(connectionString, actual);
        }
    }
}
