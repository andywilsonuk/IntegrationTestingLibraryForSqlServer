using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DatabaseActionsExtensionsTests
    {
        [Fact]
        public void TableActionsReturnsCorrectlyScopedObject()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test2;Integrated Security=True";
            var target = new DatabaseActions(connectionString);

            var actual = target.TableActions();

            Assert.Equal(connectionString, actual.ConnectionString);
        }
    }
}
