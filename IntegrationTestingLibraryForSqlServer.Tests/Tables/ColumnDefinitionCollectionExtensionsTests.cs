using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionCollectionExtensionsTests
    {
        private const string ColumnName = "C1";
        ColumnDefinitionCollection columns = new ColumnDefinitionCollection();

        [TestMethod]
        public void AddFromRaw()
        {
            var expected = new IntegerColumnDefinition(ColumnName, SqlDbType.Int) { AllowNulls = true };
            var source = new[] { new ColumnDefinitionRaw { Name = ColumnName, DataType = "Int", AllowNulls = true } };
            
            columns.AddFromRaw(source);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, columns[0]);
        }
        [TestMethod]
        public void AddBinary_Valid_Added()
        {
            var expected = new BinaryColumnDefinition(ColumnName, SqlDbType.Binary);

            var actual = columns.AddBinary(ColumnName, SqlDbType.Binary);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddDecimal_Valid_Added()
        {
            var expected = new DecimalColumnDefinition(ColumnName);

            var actual = columns.AddDecimal(ColumnName);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddInteger_Valid_Added()
        {
            var expected = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);

            var actual = columns.AddInteger(ColumnName, SqlDbType.Int);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddString_Valid_Added()
        {
            var expected = new StringColumnDefinition(ColumnName, SqlDbType.VarChar);

            var actual = columns.AddString(ColumnName, SqlDbType.VarChar);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddStandard_Valid_Added()
        {
            var expected = new StandardColumnDefinition(ColumnName, SqlDbType.DateTime);

            var actual = columns.AddStandard(ColumnName, SqlDbType.DateTime);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(expected, actual);
        }
    }
}
