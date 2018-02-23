using Xunit;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class SqlConnectionStringBuilderExtensionsTests
    {
        [Fact]
        public void IsMasterCatalog()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=master;integrated security=True");

            Assert.True(builder.IsMasterCatalog());
        }

        [Fact]
        public void IsMasterCatalogFalse()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

            Assert.False(builder.IsMasterCatalog());
        }

        [Fact]
        public void ToMasterCatalog()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

            SqlConnectionStringBuilder actual = builder.ToMasterCatalog();

            Assert.Equal(builder.DataSource, actual.DataSource);
            Assert.Equal("master", actual.InitialCatalog);
        }
    }
}
