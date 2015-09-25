using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public enum TableDataComparers
    {
        /// <summary>
        /// The shape of the data must match exactly, row and column indexes must be in the same order and have the same count.
        /// This is useful when you need ensure the actual result exactly matches the expected result.
        /// </summary>
        OrdinalRowOrdinalColumn,
        /// <summary>
        /// Row count and order must match however columns are mapped based on their name so they can appear in any order
        /// </summary>
        OrdinalRowNamedColumn,
        /// <summary>
        /// Row count and order must match however columns are mapped based on their name so they can appear in any order. 
        /// Additional actual columns are allowed.
        /// </summary>
        OrdinalRowSubsetNamedColumn,
        /// <summary>
        /// Rows can be in any order but column order and count must match.
        /// </summary>
        UnorderedRowOrdinalColumn,
        /// <summary>
        /// Rows can be in any order and columns are mapped based on their name so they can appear in any order.
        /// </summary>
        UnorderedRowNamedColumn,
        /// <summary>
        /// Rows can be in any order and columns are mapped based on their name so they can appear in any order.
        /// Additional actual columns are allowed.
        /// </summary>
        UnorderedRowSubsetNamedColumn,
        /// <summary>
        /// Rows can be in any order and additional actual rows are allowed. Column count and order must match.
        /// </summary>
        SubsetRowOrdinalColumn,
        /// <summary>
        /// Rows can be in any order and additional actual rows are allowed. Columns are mapped based on their 
        /// name so they can appear in any order. 
        /// </summary>
        SubsetRowNamedColumn,
        /// <summary>
        /// Rows can be in any order and additional actual rows are allowed. Columns are mapped based on their 
        /// name so they can appear in any order. Additional actual columns are allowed.
        /// </summary>
        SubsetRowSubsetNamedColumn
    }
}
