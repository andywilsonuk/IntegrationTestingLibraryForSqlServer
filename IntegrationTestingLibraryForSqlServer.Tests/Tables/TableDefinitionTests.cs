using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDefinitionTests
    {
        private const string TableName = "table1";
        private const string ColumnName = "c1";
        private ColumnDefinition column;
        private TableDefinition table;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new ColumnDefinition(ColumnName, SqlDbType.Int);
            table = new TableDefinition(TableName, new[] { column });
        }

        [TestMethod]
        public void TableDefinitionConstructorNullName()
        {
            new ColumnDefinition(null, SqlDbType.Int);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullTableName()
        {
            TableDefinition definition = new TableDefinition(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullSchemaName()
        {
            TableDefinition definition = new TableDefinition(TableName, null);
        }

        [TestMethod]
        public void TableDefinitionEnsureValid()
        {
            table.EnsureValid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TableDefinitionEnsureValidThrowException()
        {
            table = new TableDefinition(TableName);

            table.EnsureValid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TableDefinitionEnsureValidInvalidColumnThrowException()
        {
            column.Name = null;
            table = new TableDefinition(TableName, new[] { column });

            table.EnsureValid();
        }

        [TestMethod]
        public void TableDefinitionVerifyEqual()
        {
            var other = new TableDefinition(TableName, new[] { column });

            table.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualName()
        {
            var other = new TableDefinition("other", new[] { column });

            table.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualColumn()
        {
            var other = new TableDefinition("other", new[] { new ColumnDefinition(ColumnName, SqlDbType.NVarChar) });

            table.VerifyEqual(other);
        }

        [TestMethod]
        public void TableDefinitionEquals()
        {
            var other = new TableDefinition(TableName, new[] { column });

            bool actual = table.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsName()
        {
            var other = new TableDefinition("other", new[] { column });

            bool actual = table.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsColumn()
        {
            var other = new TableDefinition(TableName, new[] { new ColumnDefinition(column.Name, SqlDbType.NVarChar) });

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
        public void TableDefinitionEqualMixedColumnOrder()
        {
            var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
            table.Columns.Add(column2);
            var other = new TableDefinition(TableName, new[] { column2, column });

            Assert.IsTrue(table.Equals(other));
        }

        [TestMethod]
        public void TableDefinitionGetHashCode()
        {
            int expected = table.Name.ToLowerInvariant().GetHashCode() ^
                           table.Schema.ToLowerInvariant().GetHashCode();

            int actual = table.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionToString()
        {
            table.Columns.Clear();
            table.Columns.Add(new ColumnDefinition()
            {
                Name = ColumnName,
                DataType = SqlDbType.Decimal,
                Size = 10,
                DecimalPlaces = 5,
                AllowNulls = true,
                IdentitySeed = 10
            });

            string expected = new StringBuilder()
                .AppendLine("Name: table1")
                .AppendLine(string.Format("Schema: {0}", Constants.DEFAULT_SCHEMA))
                .AppendLine("Name: c1, Type: Decimal, Size: 10, Decimal Places: 5, Allow Nulls: True, Identity Seed: 10")
                .ToString();

            string actual = table.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqual()
        {
            var other = new TableDefinition(TableName, new[] { column });

            bool actual = table.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetName()
        {
            var other = new TableDefinition("other", new[] { column });

            bool actual = table.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetAdditionalColumn()
        {
            var other = new TableDefinition(TableName, new[] { column, new ColumnDefinition("c2", SqlDbType.NVarChar) });

            bool actual = table.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetMissingColumn()
        {
            var other = new TableDefinition(TableName, new[] { column });
            table.Columns.Add(new ColumnDefinition("c2", SqlDbType.NVarChar));

            bool actual = table.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqualMixedOrder()
        {
            var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
            table.Columns.Add(column2);
            var other = new TableDefinition(TableName, new[] { column2, column });

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
            var other = new TableDefinition(TableName, new[] { column });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetName()
        {
            var other = new TableDefinition("other", new[] { column });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        public void TableDefinitionVerifySubsetAdditionalColumn()
        {
            var other = new TableDefinition(TableName, new[] { column, new ColumnDefinition("c2", SqlDbType.NVarChar) });

            table.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetMissingColumn()
        {
            var other = new TableDefinition(TableName, new[] { column });
            table.Columns.Add(new ColumnDefinition("c2", SqlDbType.NVarChar));

            table.VerifyEqualOrSubsetOf(other);
        }
    }
}
