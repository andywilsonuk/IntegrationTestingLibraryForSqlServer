using System;
using Xunit;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DatabaseActionsTests
    {
        private SqlConnectionStringBuilder connectionDetails = 
            new SqlConnectionStringBuilder(@"server=(localdb)\MSSQLLocalDB;database=Test2;integrated security=True");

        [Fact]
        public void Constructor()
        {
            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            string expected = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test2;Integrated Security=True";
            Assert.Equal(expected, databaseActions.ConnectionString);
        }

        [Fact]
        public void ConstructorMaster()
        {
            connectionDetails.InitialCatalog = "master";

            Assert.Throws<ArgumentException>(() => new DatabaseActions(connectionDetails.ToString()));
        }

        [Fact]
        public void ConstructorWithoutInitialCatalogInConnectionString()
        {
            var database = new DatabaseActions(@"server=(localdb)\MSSQLLocalDB;integrated security=True", "Test");

            Assert.Equal("Test", database.Name);
        }

        [Fact]
        public void ConstructorWithInitialCatalogInConnectionString()
        {
            var database = new DatabaseActions(connectionDetails.ToString(), "Test3");

            Assert.Equal("Test3", database.Name);
        }

        [Fact]
        public void ConstructorWithBlankDatabaseName()
        {
            Assert.Throws<ArgumentException>(() => new DatabaseActions(connectionDetails.ToString(), null));
        }

        [Fact]
        public void IsLocalDb()
        {
            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            Assert.True(databaseActions.IsLocalDB);
        }

        [Fact]
        public void IsLocalDbFalse()
        {
            connectionDetails.DataSource = "server1";

            DatabaseActions databaseActions = new DatabaseActions(connectionDetails.ToString());

            Assert.False(databaseActions.IsLocalDB);
        }
    }
}
