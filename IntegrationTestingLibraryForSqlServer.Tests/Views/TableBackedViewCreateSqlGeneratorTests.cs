using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableBackedViewCreateSqlGeneratorTests
    {
        TableBackedViewCreateSqlGenerator generator = new TableBackedViewCreateSqlGenerator();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullThrowsException()
        {
            generator.Sql(null);
        }

        [TestMethod]
        public void CreateView()
        {
            var viewDefinition = new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), DatabaseObjectName.FromName("t1"));
            string expected = string.Format("CREATE VIEW [dbo].[v1] AS SELECT * FROM [dbo].[t1]");

            string actual = generator.Sql(viewDefinition);
             
            Assert.AreEqual(expected, actual);
        }
    }
}
