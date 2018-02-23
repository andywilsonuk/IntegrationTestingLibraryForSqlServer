using System;
using Xunit;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDefinitionTests
    {
        private readonly DatabaseObjectName tableName = DatabaseObjectName.FromName("table1");
        private readonly DatabaseObjectName alternativeTableName = DatabaseObjectName.FromName("other");
        private const string ColumnName = "c1";
        private const string AlternativeColumnName = "c2";
        private ColumnDefinition sourceColumn;
        private TableDefinition source;

        public TableDefinitionTests()
        {
            sourceColumn = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);
            source = new TableDefinition(tableName, new[] { sourceColumn });
        }

        [Fact]
        public void TableDefinitionConstructorNullTableNameStringThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TableDefinition((string)null));
        }

        [Fact]
        public void TableDefinitionConstructorNullTableNameThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TableDefinition((DatabaseObjectName)null));
        }

        [Fact]
        public void TableDefinitionConstructorTableName()
        {
            var definition = new TableDefinition(tableName);

            Assert.Equal(tableName, definition.Name);
        }

        [Fact]
        public void TableDefinitionVerifyEqual()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            source.VerifyEqual(other);
        }

        [Fact]
        public void TableDefinitionVerifyNotEqualNameThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            Assert.Throws<EquivalenceException>(() => source.VerifyEqual(other));
        }

        [Fact]
        public void TableDefinitionVerifyNotEqualColumnThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { new StringColumnDefinition(ColumnName, SqlDbType.NVarChar) });

            Assert.Throws<EquivalenceException>(() => source.VerifyEqual(other));
        }

        [Fact]
        public void TableDefinitionEquals()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            Assert.Equal(source, other);
        }

        [Fact]
        public void TableDefinitionNotEqualsName()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            Assert.NotEqual(source, other);
        }

        [Fact]
        public void TableDefinitionNotEqualsColumn()
        {
            var other = new TableDefinition(tableName, new[] { new StringColumnDefinition(sourceColumn.Name, SqlDbType.NVarChar) });

            Assert.NotEqual(source, other);
        }

        [Fact]
        public void TableDefinitionNotEqualNull()
        {
            bool actual = source.Equals(null);

            Assert.False(actual);
        }

        [Fact]
        public void TableDefinitionNotEqualString()
        {
            Assert.False(source.Equals(string.Empty));
        }

        [Fact]
        public void TableDefinitionEqualMixedColumnOrder()
        {
            var column2 = new StringColumnDefinition(AlternativeColumnName, SqlDbType.NVarChar);
            source.Columns.Add(column2);
            var other = new TableDefinition(tableName, new[] { column2, sourceColumn });

            Assert.Equal(source, other);
        }

        [Fact]
        public void TableDefinitionGetHashCode()
        {
            int expected = source.Name.GetHashCode();

            int actual = source.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
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

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TableDefinitionIsSubsetEqual()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.True(actual);
        }

        [Fact]
        public void TableDefinitionIsNotSubsetName()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.False(actual);
        }

        [Fact]
        public void TableDefinitionIsSubsetAdditionalColumn()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });
            other.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            bool actual = source.IsSubset(other);

            Assert.True(actual);
        }

        [Fact]
        public void TableDefinitionIsNotSubsetMissingColumn()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });
            source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            bool actual = source.IsSubset(other);

            Assert.False(actual);
        }

        [Fact]
        public void TableDefinitionIsSubsetEqualMixedOrder()
        {
            var column2 = source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);
            var other = new TableDefinition(tableName, new[] { column2, sourceColumn });

            bool actual = source.IsSubset(other);

            Assert.True(actual);
        }

        [Fact]
        public void TableDefinitionIsNotSubsetNull()
        {
            bool actual = source.IsSubset(null);

            Assert.False(actual);
        }

        [Fact]
        public void TableDefinitionVerifySubsetOfEquals()
        {
            var other = new TableDefinition(tableName, new[] { sourceColumn });

            source.VerifyEqualOrSubsetOf(other);
        }

        [Fact]
        public void TableDefinitionVerifyNotSubsetNameThrows()
        {
            var other = new TableDefinition(alternativeTableName, new[] { sourceColumn });

            Assert.Throws<EquivalenceException>(() => source.VerifyEqualOrSubsetOf(other));
        }

        [Fact]
        public void TableDefinitionVerifySubsetAdditionalColumn()
        {
            var superset = new TableDefinition(tableName, new[] { sourceColumn, new StringColumnDefinition(AlternativeColumnName, SqlDbType.NVarChar) });

            source.VerifyEqualOrSubsetOf(superset);
        }

        [Fact]
        public void TableDefinitionVerifyNotSubsetMissingColumnThrows()
        {
            var superset = new TableDefinition(tableName, new[] { sourceColumn });
            source.Columns.AddString(AlternativeColumnName, SqlDbType.NVarChar);

            Assert.Throws<EquivalenceException>(() => source.VerifyEqualOrSubsetOf(superset));
        }
    }
}
