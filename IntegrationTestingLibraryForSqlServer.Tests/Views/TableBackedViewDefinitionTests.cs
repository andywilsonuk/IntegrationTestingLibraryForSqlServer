using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableBackedViewDefinitionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableBackedViewDefinitionNullName()
        {
            var definition = new TableBackedViewDefinition(null, DatabaseObjectName.FromName("t1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableBackedViewDefinitionNullTableName()
        {
            var definition = new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), null);
        }

        [TestMethod]
        public void TableBackedViewDefinitionPropertiesSet()
        {
            var definition = new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), DatabaseObjectName.FromName("t1"));

            Assert.AreEqual("[dbo].[v1]", definition.Name.Qualified);
            Assert.AreEqual("[dbo].[t1]", definition.BackingTable.Qualified);
        }
    }
}
