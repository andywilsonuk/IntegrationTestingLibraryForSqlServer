using Xunit;
using System;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDefinitionExtensionsTests
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;database=notreal;integrated security=True";
        private TableDefinition tableDefinition = new TableDefinition("test");

        [Fact]
        public void CreateOrReplaceNullDatabase()
        {
            Assert.Throws<ArgumentNullException>(() => tableDefinition.CreateOrReplace(null));
        }

        [Fact]
        public void InsertNullDatabase()
        {
            TableData table = new TableData();

            Assert.Throws<ArgumentNullException>(() => tableDefinition.Insert(null, table));
        }

        [Fact]
        public void InsertNullTableData()
        {
            DatabaseActions database = new DatabaseActions(connectionString);
            Assert.Throws<ArgumentNullException>(() => tableDefinition.Insert(database, null));
        }

        [Fact]
        public void VerifyMatchNullDatabase()
        {
            Assert.Throws<ArgumentNullException>(() => tableDefinition.VerifyMatch(null));
        }

        [Fact]
        public void VerifyMatchOrSubsetNullDatabase()
        {
            Assert.Throws<ArgumentNullException>(() => tableDefinition.VerifyMatchOrSubset(null));
        }

        [Fact]
        public void CreateViewNullDatabase()
        {
            Assert.Throws<ArgumentNullException>(() => tableDefinition.CreateView(null, "v1"));
        }
    }
}
