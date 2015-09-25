using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataComparerStrategyFactory
    {
        public TableDataComparer Comparer(TableDataComparers strategy)
        {
            switch(strategy)
            {
                case TableDataComparers.OrdinalRowOrdinalColumn: 
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.OrdinalRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.OrdinalRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataOrdinalRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.UnorderedRowOrdinalColumn:
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.UnorderedRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.UnorderedRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataMatchEqualRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.SubsetRowOrdinalColumn:
                    return new TableDataCompositeComparer(new TableDataOrdinalColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.SubsetRowNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchEqualColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                case TableDataComparers.SubsetRowSubsetNamedColumn:
                    return new TableDataCompositeComparer(new TableDataMatchSubsetColumnComparer(), new TableDataMatchSubsetRowComparer(), new TableDataCaseSensitiveStringValueComparer());
                default:
                    throw new ArgumentException("Invalid strategy", "strategy");
            }
        }
    }
}
