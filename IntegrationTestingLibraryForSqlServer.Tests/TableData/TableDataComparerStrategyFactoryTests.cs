using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataComparerStrategyFactoryTests
    {
        private TableDataComparerStrategyFactory factory = new TableDataComparerStrategyFactory();

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataOrdinalColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataOrdinalRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchEqualColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataOrdinalRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowSubsetColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowSubsetNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchSubsetColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataOrdinalRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerSubsetRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchEqualColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchSubsetRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerSubsetRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowOrdinalColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataOrdinalColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchSubsetRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerSubsetRowSubsetNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowSubsetNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchSubsetColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchSubsetRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchEqualColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchEqualRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowOrdinalColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataOrdinalColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchEqualRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowSubsetNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowSubsetNamedColumn);

            Assert.IsInstanceOfType(actual.ColumnComparer, typeof(TableDataMatchSubsetColumnComparer));
            Assert.IsInstanceOfType(actual.RowComparer, typeof(TableDataMatchEqualRowComparer));
            Assert.IsInstanceOfType(actual.ValueComparer, typeof(TableDataCaseSensitiveStringValueComparer));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataComparerStrategyFactoryComparerBadEnum()
        {
            factory.Comparer((TableDataComparers)int.MaxValue);
        }
    }
}
