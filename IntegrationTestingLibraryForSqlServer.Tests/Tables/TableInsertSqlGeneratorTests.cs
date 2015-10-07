using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests.Tables
{
    [TestClass]
    public class TableInsertSqlGeneratorTests
    {
        private string tableName = "t1";
        private List<string> columnNames = new List<string> { "c1" };
        private TableInsertSqlGenerator generator = new TableInsertSqlGenerator();
        private const string TEST_SCHEMA = "testSchema";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertTableNullNameThrowsException()
        {
            generator.Sql(null, columnNames);
        }

        [TestMethod]
        public void InsertTable()
        {
            string expected = string.Format("INSERT INTO [{0}].[{1}] (c1) SELECT @0", Constants.DEFAULT_SCHEMA, tableName);

            string actual = generator.Sql(tableName, columnNames);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTableWithSchema()
        {
            string expected = string.Format("INSERT INTO [{0}].[{1}] (c1) SELECT @0", TEST_SCHEMA, tableName);

            string actual = generator.Sql(tableName, columnNames, TEST_SCHEMA);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertTableNullColumnNames()
        {
            generator.Sql(tableName, null);
        }

        [TestMethod]
        public void InsertTableMultipleColumns()
        {
            string expected = string.Format("INSERT INTO [{0}].[{1}] (c1,c2) SELECT @0,@1", Constants.DEFAULT_SCHEMA, tableName);
            columnNames.Add("c2");

            string actual = generator.Sql(tableName, columnNames);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertTableNoColumnNamesNullNameThrowsException()
        {
            generator.Sql(null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertTableNoColumnNamesInvalidCountThrowsException()
        {
            generator.Sql(tableName, -1);
        }

        [TestMethod]
        public void InsertTableNoColumnNames()
        {
            string expected = string.Format("INSERT INTO [{0}].[{1}] SELECT @0", Constants.DEFAULT_SCHEMA, tableName);

            string actual = generator.Sql(tableName, 1);

            Assert.AreEqual(expected, actual);
        }
    }
}
