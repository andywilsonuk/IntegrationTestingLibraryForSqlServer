﻿using IntegrationTestingLibraryForSqlServer.TableDataComparison;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableData
    {
        public TableData()
        {
            this.ColumnNames = new List<string>();
            this.Rows = new List<IList<object>>();
        }

        public IList<string> ColumnNames { get; set; }
        public IList<IList<object>> Rows { get; set; }

        public bool IsMatch(TableData other, TableDataComparer comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            return comparer.IsMatch(this, other);
        }

        public bool IsMatch(TableData other, TableDataComparers strategy)
        {
            return this.IsMatch(other, new TableDataComparerStrategyFactory().Comparer(strategy));
        }

        public void VerifyMatch(TableData other, TableDataComparer comparer)
        {
            if (!this.IsMatch(other, comparer)) throw new EquivalenceException(this.EquivalenceDetails(other));
        }

        public void VerifyMatch(TableData other, TableDataComparers strategy)
        {
            if (!this.IsMatch(other, new TableDataComparerStrategyFactory().Comparer(strategy))) throw new EquivalenceException(this.EquivalenceDetails(other));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Column names: ");
            if (this.ColumnNames != null) sb.Append(string.Join(", ", this.ColumnNames));
            sb.AppendLine();

            foreach (var row in this.Rows)
                sb.AppendLine(string.Join(", ", row));
            return sb.ToString();
        }

        private string EquivalenceDetails(TableData actual)
        {
            return new StringBuilder()
                .AppendLine("Table data mismatch.")
                .AppendLine("Expected:")
                .Append(this.ToString())
                .AppendLine("Actual:")
                .Append(actual.ToString())
                .ToString();
        }
    }
}