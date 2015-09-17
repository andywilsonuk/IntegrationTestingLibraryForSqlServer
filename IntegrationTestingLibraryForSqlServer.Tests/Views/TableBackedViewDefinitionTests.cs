using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableBackedViewDefinitionTests
    {
        private TableBackedViewDefinition definition;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableBackedViewDefinitionNullName()
        {
            this.definition = new TableBackedViewDefinition(null, "t1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableBackedViewDefinitionNullTableName()
        {
            this.definition = new TableBackedViewDefinition("v1", null);
        }

        [TestMethod]
        public void TableBackedViewDefinitionPropertiesSet()
        {
            this.definition = new TableBackedViewDefinition("v1", "t1");

            Assert.AreEqual("v1", this.definition.Name);
            Assert.AreEqual("t1", this.definition.BackingTable);
        }
    }
}
