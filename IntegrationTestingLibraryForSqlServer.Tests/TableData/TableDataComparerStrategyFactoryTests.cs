using System;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataComparerStrategyFactoryTests
    {
        private TableDataComparerStrategyFactory factory = new TableDataComparerStrategyFactory();

        [Fact]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.IsType<TableDataOrdinalColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataOrdinalRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowNamedColumn);

            Assert.IsType<TableDataMatchEqualColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataOrdinalRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerOrdinalRowSubsetColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.OrdinalRowSubsetNamedColumn);

            Assert.IsType<TableDataMatchSubsetColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataOrdinalRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerSubsetRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowNamedColumn);

            Assert.IsType<TableDataMatchEqualColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchSubsetRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerSubsetRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowOrdinalColumn);

            Assert.IsType<TableDataOrdinalColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchSubsetRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerSubsetRowSubsetNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.SubsetRowSubsetNamedColumn);

            Assert.IsType<TableDataMatchSubsetColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchSubsetRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowNamedColumn);

            Assert.IsType<TableDataMatchEqualColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchEqualRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowOrdinalColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowOrdinalColumn);

            Assert.IsType<TableDataOrdinalColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchEqualRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerUnorderedRowSubsetNamedColumn()
        {
            var actual = (TableDataCompositeComparer)factory.Comparer(TableDataComparers.UnorderedRowSubsetNamedColumn);

            Assert.IsType<TableDataMatchSubsetColumnComparer>(actual.ColumnComparer);
            Assert.IsType<TableDataMatchEqualRowComparer>(actual.RowComparer);
            Assert.IsType<TableDataValueComparerPipeline>(actual.ValueComparer);
        }

        [Fact]
        public void TableDataComparerStrategyFactoryComparerBadEnum()
        {
            Assert.Throws<ArgumentException>(() => factory.Comparer((TableDataComparers)int.MaxValue));
        }
    }
}
