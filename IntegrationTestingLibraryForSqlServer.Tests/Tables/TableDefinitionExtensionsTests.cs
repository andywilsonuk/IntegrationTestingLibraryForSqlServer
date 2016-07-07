using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDefinitionExtensionsTests
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;database=notreal;integrated security=True";
        private TableDefinition tableDefinition = new TableDefinition("test");

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOrReplaceNullDatabase()
        {
            tableDefinition.CreateOrReplace(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNullDatabase()
        {
            TableData table = new TableData();

            tableDefinition.Insert(null, table);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNullTableData()
        {
            DatabaseActions database = new DatabaseActions(connectionString);
            tableDefinition.Insert(database, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyMatchNullDatabase()
        {
            tableDefinition.VerifyMatch(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyMatchOrSubsetNullDatabase()
        {
            tableDefinition.VerifyMatchOrSubset(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateViewNullDatabase()
        {
            tableDefinition.CreateView(null, "v1");
        }
    }
}
