using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDefinitionTests
    {
        private readonly DatabaseObjectName tableName = DatabaseObjectName.FromName("table1");
        private const string ColumnName = "c1";
        private ColumnDefinition column;
        private TableDefinition table;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new ColumnDefinition(ColumnName, SqlDbType.Int);
            table = new TableDefinition(tableName, new[] { column });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullTableNameString()
        {
            new TableDefinition((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullTableName()
        {
            new TableDefinition((DatabaseObjectName)null);
        }

        [TestMethod]
        public void TableDefinitionVerifyEqual()
        {
            var other = new TableDefinition(tableName, new[] { column });

            table.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualName()
        {
            var other = new TableDefinition(DatabaseObjectName.FromName("other"), new[] { column });

            table.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualColumn()
        {
            var other = new TableDefinition(DatabaseObjectName.FromName("other"), new[] { new ColumnDefinition(ColumnName, SqlDbType.NVarChar) });

            table.VerifyEqual(other);
        }

        [TestMethod]
        public void TableDefinitionEquals()
        {
            var other = new TableDefinition(tableName, new[] { column });

            bool actual = table.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsName()
        {
            var other = new TableDefinition(DatabaseObjectName.FromName("other"), new[] { column });

            bool actual = table.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsColumn()
        {
            var other = new TableDefinition(tableName, new[] { new ColumnDefinition(column.Name, SqlDbType.NVarChar) });

            bool actual = table.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualNull()
        {
            bool actual = table.Equals(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualString()
        {
            bool actual = table.Equals(string.Empty);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionEqualMixedColumnOrder()
        {
            var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
            table.Columns.Add(column2);
            var other = new TableDefinition(tableName, new[] { column2, column });

            Assert.IsTrue(table.Equals(other));
        }

        [TestMethod]
        public void TableDefinitionGetHashCode()
        {
            int expected = table.Name.GetHashCode();

            int actual = table.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionToString()
        {
            table.Columns.Clear();
            table.Columns.Add(new SizeableColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                Size = 10,
                AllowNulls = true,
            });

            string expected = new StringBuilder()
                .AppendLine("Name: [dbo].[table1]")
                .AppendLine("Name: c1, Type: NVarChar, Allow Nulls: True, Size: 10")
                .ToString();

            string actual = table.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqual()
        {
            var other = new TableDefinition(tableName, new[] { column });

            bool actual = table.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetName()
        {
            var other = new TableDefinition(DatabaseObjectName.FromName("other"), new[] { column });

            bool actual = table.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetAdditionalColumn()
        {
            var other = new TableDefinition(tableName, new[] { column, new ColumnDefinition("c2", SqlDbType.NVarChar) });

            bool actual = table.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetMissingColumn()
        {
            var other = new TableDefinition(tableName, new[] { column });
            table.Columns.Add(new ColumnDefinition("c2", SqlDbType.NVarChar));

            bool actual = table.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqualMixedOrder()
        {
            var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
            table.Columns.Add(column2);
            var other = new TableDefinition(tableName, new[] { column2, column });

            bool actual = table.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetNull()
        {
            bool actual = table.IsSubset(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionVerifySubsetOfEquals()
        {
            var other = new TableDefinition(tableName, new[] { column });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetName()
        {
            var other = new TableDefinition(DatabaseObjectName.FromName("other"), new[] { column });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        public void TableDefinitionVerifySubsetAdditionalColumn()
        {
            var other = new TableDefinition(tableName, new[] { column, new ColumnDefinition("c2", SqlDbType.NVarChar) });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetMissingColumn()
        {
            var other = new TableDefinition(tableName, new[] { column });
            table.Columns.Add(new ColumnDefinition("c2", SqlDbType.NVarChar));

            table.VerifyEqualOrSubsetOf(other);
        }
    }
}
