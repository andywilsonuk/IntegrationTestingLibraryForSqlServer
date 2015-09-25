﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class CollectionPopulatedTableDataTests
    {
        private List<string> columnNames;

        [TestInitialize]
        public void TestInitialize()
        {
            this.columnNames = new List<string> { "a", "b" };
        }

        [TestMethod]
        public void CollectionPopulatedTableDataStringValues()
        {
            var expectedColumnNames = new List<string> { "a", "b" };
            var expectedRows = new List<IList<object>> { new List<object> { "1", "2" } };
            var sourceRows = new List<List<string>> { new List<string> { "1", "2" } };

            var tableData = new CollectionPopulatedTableData(columnNames, sourceRows);

            Assert.IsTrue(expectedColumnNames.SequenceEqual(tableData.ColumnNames));
            tableData.Rows[0][0] = expectedRows[0][0];
        }

        [TestMethod]
        public void CollectionPopulatedTableDataObjectValues()
        {
            var expectedColumnNames = new List<string> { "a", "b" };
            var expectedRows = new List<IList<object>> { new List<object> { "1", "2" } };
            var sourceRows = new List<List<object>> { new List<object> { "1", "2" } };

            var tableData = new CollectionPopulatedTableData(columnNames, sourceRows);

            Assert.IsTrue(expectedColumnNames.SequenceEqual(tableData.ColumnNames));
            tableData.Rows[0][0] = expectedRows[0][0];
        }
    }
}