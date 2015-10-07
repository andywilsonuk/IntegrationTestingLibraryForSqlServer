using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableBackedViewCreateSqlGeneratorTests
    {
        TableBackedViewCreateSqlGenerator generator = new TableBackedViewCreateSqlGenerator();
        TableBackedViewDefinition viewDefinition = new TableBackedViewDefinition("v1", "t1");
        private const string TEST_SCHEMA = "testSchema";
        TableBackedViewDefinition viewDefinitionWithSchema = new TableBackedViewDefinition("v1", "t1", TEST_SCHEMA);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullThrowsException()
        {
            generator.Sql(null);
        }

        [TestMethod]
        public void CreateView()
        {
            string expected = string.Format("CREATE VIEW [{0}].[v1] AS SELECT * FROM [{0}].[t1]", Constants.DEFAULT_SCHEMA); ;

            string actual = generator.Sql(viewDefinition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateViewWithSchema()
        {
            string expected = string.Format("CREATE VIEW [{0}].[v1] AS SELECT * FROM [{0}].[t1]", TEST_SCHEMA); ;

            string actual = generator.Sql(viewDefinitionWithSchema);

            Assert.AreEqual(expected, actual);
        }
    }
}
