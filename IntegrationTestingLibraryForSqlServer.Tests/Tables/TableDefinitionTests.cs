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
        private readonly DatabaseObjectName alternativeTableName = DatabaseObjectName.FromName("other");
        private const string ColumnName = "c1";
        private const string AlternativeColumnName = "c2";
        private ColumnDefinition sourceColumn;
        private TableDefinition source;

        [TestInitialize]
        public void TestInitialize()
        {
            sourceColumn = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);
            source = new TableDefinition(tableName, new[] { sourceColumn });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullTableNameStringThrows()
        {
            new TableDefinition((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDefinitionConstructorNullTableNameThrows()
        {
            new TableDefinition((DatabaseObjectName)null);
        }

        [TestMethod]
        public void TableDefinitionConstructorTableName()
        {
            var definition = new TableDefinition(tableName);

            Assert.AreEqual(tableName, definition.Name);
        }

        [TestMethod]
        public void TableDefinitionVerifyEqual()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            source.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualNameThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            source.VerifyEqual(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotEqualColumnThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { new StringColumnDefinition(ColumnName, SqlDbType.NVarChar) });

            source.VerifyEqual(other);
        }

        [TestMethod]
        public void TableDefinitionEquals()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            Assert.AreEqual(source, other);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsName()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            Assert.AreNotEqual(source, other);
        }

        [TestMethod]
        public void TableDefinitionNotEqualsColumn()
        {
            var other = new TableDefinition(tableName, new[] { new StringColumnDefinition(sourceColumn.Name, SqlDbType.NVarChar) });

            Assert.AreNotEqual(source, other);
        }

        [TestMethod]
        public void TableDefinitionNotEqualNull()
        {
            bool actual = source.Equals(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionNotEqualString()
        {
            Assert.AreNotEqual(source, string.Empty);
        }

        [TestMethod]
        public void TableDefinitionEqualMixedColumnOrder()
        {
            var column2 = new StringColumnDefinition(AlternativeColumnName, SqlDbType.NVarChar);
            source.Columns.Add(column2);
            var other = new TableDefinition(tableName, new[] { column2, sourceColumn });

            Assert.AreEqual(source, other);
        }

        [TestMethod]
        public void TableDefinitionGetHashCode()
        {
            int expected = source.Name.GetHashCode();

            int actual = source.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionToString()
        {
            source.Columns.Clear();
            var column = source.Columns.AddString(ColumnName, SqlDbType.NVarChar);
            column.Size = 10;
            column.AllowNulls = true;

            string expected = new StringBuilder()
                .AppendLine("Name: [dbo].[table1]")
                .AppendLine("Name: c1, Type: NVarChar, Allow Nulls: True, Size: 10")
                .ToString();

            string actual = source.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqual()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetName()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetAdditionalColumn()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });
            other.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            bool actual = source.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetMissingColumn()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });
            source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            bool actual = source.IsSubset(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionIsSubsetEqualMixedOrder()
        {
            var column2 = source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);
            var other = new TableDefinition(tableName, new[] { column2, sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDefinitionIsNotSubsetNull()
        {
            bool actual = source.IsSubset(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDefinitionVerifySubsetOfEquals()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            source.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetNameThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            source.VerifyEqualOrSubsetOf(other);
        }

        [TestMethod]
        public void TableDefinitionVerifySubsetAdditionalColumn()
        {
            var superset = new TableDefinition(tableName, new[] { sourceColumn, new StringColumnDefinition(AlternativeColumnName, SqlDbType.NVarChar) });

            source.VerifyEqualOrSubsetOf(superset);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDefinitionVerifyNotSubsetMissingColumnThrows()
        {
            var superset = new TableDefinition(tableName, new[] { sourceColumn });
            source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            source.VerifyEqualOrSubsetOf(superset);
        }
    }
}
