using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests.Tables
{
    [TestClass]
    public class TableInsertSqlGeneratorTests
    {
        private DatabaseObjectName tableName = DatabaseObjectName.FromName("t1");
        private List<string> columnNames = new List<string> { "c1" };
        private TableInsertSqlGenerator generator = new TableInsertSqlGenerator();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertTableNullNameThrowsException()
        {
            generator.Sql(null, columnNames);
        }

        [TestMethod]
        public void InsertTable()
        {
            string expected = string.Format("INSERT INTO {0} (c1) SELECT @0", tableName.Qualified);

            string actual = generator.Sql(tableName, columnNames);

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
            string expected = string.Format("INSERT INTO {0} (c1,c2) SELECT @0,@1", tableName.Qualified);
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
            string expected = string.Format("INSERT INTO {0} SELECT @0", tableName.Qualified);

            string actual = generator.Sql(tableName, 1);

            Assert.AreEqual(expected, actual);
        }
    }
}
