using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ViewCreateSqlGeneratorTests
    {
        ViewCreateSqlGenerator generator = new ViewCreateSqlGenerator();
        ViewDefinition viewDefinition = new ViewDefinition("v1", "t1");

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullThrowsException()
        {
            generator.Sql(null);
        }

        [TestMethod]
        public void CreateView()
        {
            string expected = "CREATE VIEW [v1] AS SELECT * FROM [t1]";
            
            string actual = generator.Sql(viewDefinition);

            Assert.AreEqual(expected, actual);
        }
    }
}
