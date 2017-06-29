using System;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataComparerStrategyFactory
    {
        public TableDataComparer Comparer(TableDataComparers strategy)
        {
            switch(strategy)
            {
                case TableDataComparers.OrdinalRowOrdinalColumn:
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.OrdinalRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.OrdinalRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.UnorderedRowOrdinalColumn:
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.UnorderedRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.UnorderedRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.SubsetRowOrdinalColumn:
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.SubsetRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataValueComparerPipeline());
                case TableDataComparers.SubsetRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataValueComparerPipeline());
                default:
                    throw new ArgumentException("Invalid strategy", "strategy");
            }
        }
    }
}
