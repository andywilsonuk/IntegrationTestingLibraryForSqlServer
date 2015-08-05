using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDefinitionTests
    {
        private ColumnDefinition column;
        private TableDefinition table;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new ColumnDefinition("c1", SqlDbType.Int);
            table = new TableDefinition("table1", new[] { column });
        }

        [TestMethod]
        public void TableDefinitionConstructor()
        {
            new ColumnDefinition("c1", SqlDbType.Int);
        }

        [TestMethod]
        public void TableDefinitionConstructorNullName()
        {
            new ColumnDefinition(null, SqlDbType.Int);
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
            table = new TableDefinition("table1");

            table.EnsureValid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TableDefinitionEnsureValidInvalidColumnThrowException()
        {
            column.Name = null;
            table = new TableDefinition("table1", new[] { column} );

            table.EnsureValid();
        }

        [TestMethod]
        public void TableDefinitionVerifyEqual()
        {
            var other = new TableDefinition("table1", new[] { column });

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
            var other = new TableDefinition("other", new[] { new ColumnDefinition("c1", SqlDbType.NVarChar) });

            table.VerifyEqual(other);
        }

        [TestMethod]
        public void TableDefinitionEquals()
        {
            var other = new TableDefinition("table1", new[] { column });

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
            var other = new TableDefinition("other", new[] { new ColumnDefinition("c1", SqlDbType.NVarChar) });

            bool actual = table.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionGetHashCode()
        {
            int expected = table.Name.ToLowerInvariant().GetHashCode();

            int actual = table.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionToString()
        {
            table.Columns.Clear();
            table.Columns.Add(new ColumnDefinition()
            {
                Name = "c1",
                DataType = SqlDbType.Decimal,
                Size = 10,
                Precision = 5,
                AllowNulls = true,
                IdentitySeed = 10
            });

            string expected = new StringBuilder()
                .AppendLine("Name: table1")
                .AppendLine("Name: c1, Type: Decimal, Size: 10, Precision: 5, Allow Nulls: True, Identity Seed: 10")
                .ToString();

            string actual = table.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
