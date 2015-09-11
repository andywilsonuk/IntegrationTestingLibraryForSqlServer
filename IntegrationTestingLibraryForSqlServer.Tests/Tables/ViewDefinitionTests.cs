using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ViewDefinitionTests
    {
        private ViewDefinition definition;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ViewDefinitionNullName()
        {
            this.definition = new ViewDefinition(null, "t1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ViewDefinitionNullTableName()
        {
            this.definition = new ViewDefinition("v1", null);
        }

        [TestMethod]
        public void ViewDefinitionPropertiesSet()
        {
            this.definition = new ViewDefinition("v1", "t1");

            Assert.AreEqual("v1", this.definition.Name);
            Assert.AreEqual("t1", this.definition.BackingTable);
        }
    }
}
