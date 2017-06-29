using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ObjectPopulatedTableDataTests
    {
        [TestMethod]
        public void PopulateFromObject()
        {
            var source = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("a", 1),
                new Tuple<string, int>("b", 2),
            };

            var tableData = new ObjectPopulatedTableData<Tuple<string, int>>(source);

            Assert.AreEqual(2, tableData.ColumnNames.Count);
            Assert.IsTrue(tableData.ColumnNames.Contains("Item1"));
            Assert.IsTrue(tableData.ColumnNames.Contains("Item2"));
            Assert.AreEqual("a", tableData.Rows[0][0]);
            Assert.AreEqual(1, tableData.Rows[0][1]);
            Assert.AreEqual("b", tableData.Rows[1][0]);
            Assert.AreEqual(2, tableData.Rows[1][1]);
        }
    }
}
